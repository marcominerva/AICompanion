﻿using AICompanion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AICompanion
{
	public partial class App : Application
	{
        public static bool IsPausing { get; set; }

        public App ()
		{
			InitializeComponent();

            var start = new MainPage();
            MainPage = new NavigationPage(start);
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
