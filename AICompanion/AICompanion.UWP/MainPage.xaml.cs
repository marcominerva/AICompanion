using Plugin.CustomVisionEngine;
using Plugin.CustomVisionEngine.Models;

namespace AICompanion.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new AICompanion.App());

            _ = CrossOfflineClassifier.Current.InitializeAsync(ModelType.General, "ms-appx:///Assets/Models/Computer.onnx");
        }
    }
}
