using AICompanion.Common;
using AICompanion.Services;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Plugin.Media.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AICompanion.ViewModels
{
    public class VisionViewModel : ViewModelBase
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

        public VisionViewModel(IMediaService mediaService)
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

                    var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(SettingsService.VisionSubscriptionKey))
                    {
                        Endpoint = $"https://{SettingsService.VisionRegion}.api.cognitive.microsoft.com"
                    };

                    var result = await client.DescribeImageInStreamAsync(file.GetStream());
                    Description = $"{result.Captions.FirstOrDefault()?.Text} ({result.Captions.FirstOrDefault()?.Confidence:P1})";

                    file.Dispose();
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
    }
}
