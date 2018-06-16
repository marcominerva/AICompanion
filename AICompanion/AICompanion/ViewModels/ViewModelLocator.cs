using Acr.UserDialogs;
using AICompanion.Common;
using AICompanion.Services;
using AICompanion.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Globalization;
using Xamarin.Forms;

namespace AICompanion.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(Constants.MainPage, typeof(MainPage));
            navigationService.Configure(Constants.SettingsPage, typeof(SettingsPage));

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IUserDialogs>(() => UserDialogs.Instance);
            SimpleIoc.Default.Register<IPermissionService, PermissionService>();
            SimpleIoc.Default.Register<IMediaService, MediaService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();

            SimpleIoc.Default.Register<VisionViewModel>();
            SimpleIoc.Default.Register<FaceViewModel>();
            SimpleIoc.Default.Register<CustomVisionViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public VisionViewModel VisionViewModel => SimpleIoc.Default.GetInstance<VisionViewModel>();

        public FaceViewModel FaceViewModel => SimpleIoc.Default.GetInstance<FaceViewModel>();

        public CustomVisionViewModel CustomVisionViewModel => SimpleIoc.Default.GetInstance<CustomVisionViewModel>();

        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
    }
}
