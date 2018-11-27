using Agenda.Interfaces;
using Agenda.UWP.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.UI.Notifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(Message))]
namespace Agenda.UWP.Services
{
    public class Message : IMessage
    {
        private void ShowAlert(string message, int duration)
        {
            ToastContent content = new ToastContent()
            {
                Duration = duration == 0 ? ToastDuration.Short : ToastDuration.Long,
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                                {
                                    Text = message
                                }
                        }
                    }
                }
            };

            var toast = new ToastNotification(content.GetXml())
            {
                ExpirationTime = DateTime.Now.AddSeconds(20)
            };
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public void LongAlert(string message)
        {
            this.ShowAlert(message, 1);
        }

        public void ShortAlert(string message)
        {
            this.ShowAlert(message, 0);
        }
    }
}