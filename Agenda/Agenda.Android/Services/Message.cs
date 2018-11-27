using Agenda.Droid.Services;
using Agenda.Interfaces;
using Android.Widget;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(Message))]

namespace Agenda.Droid.Services
{
    public class Message : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}