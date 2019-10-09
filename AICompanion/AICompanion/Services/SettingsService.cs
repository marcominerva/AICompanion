using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace AICompanion.Services
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly ISettings settings;

        public SettingsService()
        {
            settings = CrossSettings.Current;
        }

        public string VisionRegion
        {
            get => settings.GetValueOrDefault(nameof(VisionRegion), null);
            set => settings.AddOrUpdateValue(nameof(VisionRegion), value);
        }

        public string VisionSubscriptionKey
        {
            get => settings.GetValueOrDefault(nameof(VisionSubscriptionKey), null);
            set => settings.AddOrUpdateValue(nameof(VisionSubscriptionKey), value);
        }

        public string FaceRegion
        {
            get => settings.GetValueOrDefault(nameof(FaceRegion), null);
            set => settings.AddOrUpdateValue(nameof(FaceRegion), value);
        }

        public string FaceSubscriptionKey
        {
            get => settings.GetValueOrDefault(nameof(FaceSubscriptionKey), null);
            set => settings.AddOrUpdateValue(nameof(FaceSubscriptionKey), value);
        }

        public string CustomVisionRegion
        {
            get => settings.GetValueOrDefault(nameof(CustomVisionRegion), null);
            set => settings.AddOrUpdateValue(nameof(CustomVisionRegion), value);
        }

        public string CustomVisionProjectName
        {
            get => settings.GetValueOrDefault(nameof(CustomVisionProjectName), null);
            set => settings.AddOrUpdateValue(nameof(CustomVisionProjectName), value);
        }

        public string CustomVisionPredictionKey
        {
            get => settings.GetValueOrDefault(nameof(CustomVisionPredictionKey), null);
            set => settings.AddOrUpdateValue(nameof(CustomVisionPredictionKey), value);
        }

        public string CustomVisionIterationId
        {
            get => settings.GetValueOrDefault(nameof(CustomVisionIterationId), Guid.Empty.ToString("D"));
            set => settings.AddOrUpdateValue(nameof(CustomVisionIterationId), value);
        }
    }
}