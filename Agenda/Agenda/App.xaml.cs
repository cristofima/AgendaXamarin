using Agenda.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Agenda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current.Resources["backgroundColor"] = AppSettings.BackgroundColor;

            MainPage = new NavigationPage(new PersonasPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
