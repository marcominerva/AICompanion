namespace AICompanion.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public string VisionRegion
        {
            get => SettingsService.VisionRegion;
            set => SettingsService.VisionRegion = value;
        }

        public string VisionSubscriptionKey
        {
            get => SettingsService.VisionSubscriptionKey;
            set => SettingsService.VisionSubscriptionKey = value;
        }

        public string FaceRegion
        {
            get => SettingsService.FaceRegion;
            set => SettingsService.FaceRegion = value;
        }

        public string FaceSubscriptionKey
        {
            get => SettingsService.FaceSubscriptionKey;
            set => SettingsService.FaceSubscriptionKey = value;
        }

        public string CustomVisionRegion
        {
            get => SettingsService.CustomVisionRegion;
            set => SettingsService.CustomVisionRegion = value;
        }

        public string CustomVisionProjectName
        {
            get => SettingsService.CustomVisionProjectName;
            set => SettingsService.CustomVisionProjectName = value;
        }

        public string CustomVisionPredictionKey
        {
            get => SettingsService.CustomVisionPredictionKey;
            set => SettingsService.CustomVisionPredictionKey = value;
        }

        public string CustomVisionIterationId
        {
            get => SettingsService.CustomVisionIterationId;
            set => SettingsService.CustomVisionIterationId = value;
        }
    }
}
