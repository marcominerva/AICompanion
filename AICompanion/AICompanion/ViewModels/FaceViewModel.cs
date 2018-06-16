using AICompanion.Common;
using AICompanion.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.CustomVisionEngine;
using Plugin.CustomVisionEngine.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace AICompanion.ViewModels
{
    public class FaceViewModel : ViewModelBase
    {
        private readonly IMediaService mediaService;

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => Set(ref imagePath, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        public AutoRelayCommand TakePhotoCommand { get; private set; }

        public AutoRelayCommand PickPhotoCommand { get; private set; }

        public AutoRelayCommand SettingsCommand { get; private set; }

        public FaceViewModel(IMediaService mediaService)
        {
            this.mediaService = mediaService;

            CreateCommands();
        }

        private void CreateCommands()
        {
            TakePhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.TakePhotoAsync()));
            PickPhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.PickPhotoAsync()));
            SettingsCommand = new AutoRelayCommand(() => NavigationService.NavigateTo(Constants.SettingsPage));
        }

        private async Task AnalyzePhotoAsync(Func<Task<MediaFile>> action)
        {
            IsBusy = true;

            try
            {
                var file = await action.Invoke();
                if (file != null)
                {
                    // Clean up previous results.
                    Description = null;
                    ImagePath = file.Path;

                    var client = new FaceAPI(new ApiKeyServiceClientCredentials(SettingsService.FaceSubscriptionKey))
                    {
                        AzureRegion = AzureRegions.Westeurope
                    };

                    var faces = await client.Face.DetectWithStreamAsync(file.GetStream(), returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Emotion });
                    file.Dispose();

                    if (faces.Any())
                    {
                        // Try to identify faces in the image.
                        IEnumerable<IdentifyResult> faceIdentificationResult = null;
                        var identifyPersonGroupId = await GetPersonGroupAsync(client);
                        if (!string.IsNullOrWhiteSpace(identifyPersonGroupId))
                        {
                            var faceIds = faces.Select(face => face.FaceId).Cast<Guid>().ToList();
                            faceIdentificationResult = await client.Face.IdentifyAsync(identifyPersonGroupId, faceIds);
                        }

                        var result = new StringBuilder();
                        foreach (var face in faces)
                        {
                            // Add standard face information to the result.
                            result.Append($"{face.FaceAttributes.Gender}, {face.FaceAttributes.Age} years, {ToRankedList(face.FaceAttributes.Emotion).First().Key}{Environment.NewLine}");

                            // Check if there is a candidate (i.e., a known person) in the identification result.
                            var candidate = faceIdentificationResult?.FirstOrDefault(r => r.FaceId == face.FaceId)?.Candidates.FirstOrDefault();
                            if (candidate != null)
                            {
                                // Get the person name.
                                var person = await client.PersonGroupPerson.GetAsync(identifyPersonGroupId, candidate.PersonId);
                                result.Append($"{person.Name}{Environment.NewLine}{Environment.NewLine}");
                            }
                        }

                        Description = result.ToString();
                    }
                    else
                    {
                        Description = "No face recognized";
                    }
                }
            }
            catch (Exception ex)
            {
                await DialogService.AlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<string> GetPersonGroupAsync(FaceAPI faceService)
        {
            try
            {
                var personGroups = await faceService.PersonGroup.ListAsync();
                var identifyPersonGroupId = (personGroups.FirstOrDefault(p => p.Name.ToLowerInvariant() == "_default" || p.UserData.ToLowerInvariant() == "_default") ?? personGroups.FirstOrDefault())?.PersonGroupId;

                return identifyPersonGroupId;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Create a sorted key-value pair of emotions and the corresponding scores, sorted from highest score on down.
        /// To make the ordering stable, the score is the primary key, and the name is the secondary key.
        /// </summary>
        private IEnumerable<KeyValuePair<string, double>> ToRankedList(Emotion emotion)
        {
            return new Dictionary<string, double>()
            {
                { "Anger", emotion.Anger },
                { "Contempt", emotion.Contempt },
                { "Disgust", emotion.Disgust },
                { "Fear", emotion.Fear },
                { "Happiness", emotion.Happiness },
                { "Neutral", emotion.Neutral },
                { "Sadness", emotion.Sadness },
                { "Surprise", emotion.Surprise }
            }
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .ToList();
        }
    }
}
