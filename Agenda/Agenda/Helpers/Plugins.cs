using System;
using Xamarin.Essentials;

namespace Agenda.Helpers
{
    public class Plugins
    {
        public static void Vibrate()
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}