using Agenda.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PreferencesViewModel : BaseViewModel
    {
        public string colorActual;

        public string ColorActual
        {
            get
            {
                return colorActual;
            }
            set
            {
                SetValue(ref colorActual, value);
            }
        }

        public List<string> ColorList { get; set; }

        public PreferencesViewModel()
        {
            this.ColorList = new List<string>
            {
                 "Aqua","Black",
                 "Blue", "Fucshia",
                 "Gray",  "Green",
                 "Lime",  "Maroon",
                 "Navy",  "Olive",
                 "Purple",  "Red",
                 "Silver", "Teal",
                 "White",  "Yellow"
            };
            this.ColorActual = Preferences.Get("BackgroundColor", "White");
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            Preferences.Set("BackgroundColor", this.ColorActual);
            DependencyService.Get<IMessage>().ShortAlert("Preferencias guardadas");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
