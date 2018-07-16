using AICompanion.Common;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Threading.Tasks;

namespace AICompanion.Services
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private const string CUSTOM_VISION_PREDICTION_KEY = nameof(CUSTOM_VISION_PREDICTION_KEY);
        private const string CUSTOM_VISION_PROJECT_ID = nameof(CUSTOM_VISION_PROJECT_ID);
        private const string VISION_SUBSCRIPTION_KEY = nameof(VISION_SUBSCRIPTION_KEY);
        private const string FACE_SUBSCRIPTION_KEY = nameof(FACE_SUBSCRIPTION_KEY);

        private readonly ISettings settings;

        public SettingsService()
        {
            settings = CrossSettings.Current;
        }

        public string VisionSubscriptionKey
        {
            get => settings.GetValueOrDefault(VISION_SUBSCRIPTION_KEY, null);
            set => settings.AddOrUpdateValue(VISION_SUBSCRIPTION_KEY, value);
        }

        public string FaceSubscriptionKey
        {
            get => settings.GetValueOrDefault(FACE_SUBSCRIPTION_KEY, null);
            set => settings.AddOrUpdateValue(FACE_SUBSCRIPTION_KEY, value);
        }

        public string CustomVisionPredictionKey
        {
            get => settings.GetValueOrDefault(CUSTOM_VISION_PREDICTION_KEY, null);
            set => settings.AddOrUpdateValue(CUSTOM_VISION_PREDICTION_KEY, value);
        }

        public string CustomVisionProjectId
        {
            get => settings.GetValueOrDefault(CUSTOM_VISION_PROJECT_ID, null);
            set => settings.AddOrUpdateValue(CUSTOM_VISION_PROJECT_ID, value);
        }
    }
}