﻿using AICompanion.Common;
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

namespace AICompanion.ViewModels
{
    public class CustomVisionViewModel : ViewModelBase
    {
        private readonly IMediaService mediaService;

        private IEnumerable<string> predictions;
        public IEnumerable<string> Predictions
        {
            get => predictions;
            set => Set(ref predictions, value);
        }

        private bool isOffline;
        public bool IsOffline
        {
            get => isOffline;
            set => Set(ref isOffline, value);
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => Set(ref imagePath, value);
        }

        public AutoRelayCommand TakePhotoCommand { get; private set; }

        public AutoRelayCommand PickPhotoCommand { get; private set; }

        public AutoRelayCommand SettingsCommand { get; private set; }

        public CustomVisionViewModel(IMediaService mediaService)
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
                    Predictions = null;
                    ImagePath = file.Path;

                    // Check whether to use the online or offline version of the prediction model.
                    IEnumerable<Recognition> predictionsRecognized = null;
                    if (isOffline)
                    {
                        var classifier = CrossOfflineClassifier.Current;
                        predictionsRecognized = await classifier.RecognizeAsync(file.GetStream(), file.Path);
                    }
                    else
                    {
                        var classifier = CrossOnlineClassifier.Current;
                        predictionsRecognized = await classifier.RecognizeAsync(SettingsService.CustomVisionPredictionKey, Guid.Parse(SettingsService.CustomVisionProjectId), file.GetStream());
                    }

                    Predictions = predictionsRecognized.Select(p => $"{p.Tag}: {p.Probability:P1}");
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
