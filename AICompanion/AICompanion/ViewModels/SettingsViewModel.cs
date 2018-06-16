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

namespace AICompanion.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public string VisionSubscriptionKey
        {
            get => SettingsService.VisionSubscriptionKey;
            set => SettingsService.VisionSubscriptionKey = value;
        }

        public string FaceSubscriptionKey
        {
            get => SettingsService.FaceSubscriptionKey;
            set => SettingsService.FaceSubscriptionKey = value;
        }

        public string CustomVisionPredictionKey
        {
            get => SettingsService.CustomVisionPredictionKey;
            set => SettingsService.CustomVisionPredictionKey = value;
        }

        public string CustomVisionProjectId
        {
            get => SettingsService.CustomVisionProjectId;
            set => SettingsService.CustomVisionProjectId = value;
        }
    }
}
