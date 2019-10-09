namespace AICompanion.Services
{
    public interface ISettingsService
    {
        string VisionRegion { get; set; }

        string VisionSubscriptionKey { get; set; }

        string FaceRegion { get; set; }

        string FaceSubscriptionKey { get; set; }

        string CustomVisionRegion { get; set; }

        string CustomVisionProjectName { get; set; }

        string CustomVisionPredictionKey { get; set; }

        string CustomVisionIterationId { get; set; }
    }
}
