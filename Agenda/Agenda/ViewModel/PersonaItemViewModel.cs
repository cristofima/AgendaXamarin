using Agenda.Helpers;
using Agenda.Models;
using Agenda.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PersonaItemViewModel : Persona
    {
        public PersonaItemViewModel(Persona per) : base(per) { }

        public PersonaItemViewModel() { }

        #region Comandos
        public ICommand SelectCommand
        {
            get
            {
                return new RelayCommand(GoItem);
            }
        }

        public ICommand CallCommand
        {
            get
            {
                return new RelayCommand(Call);
            }
        }

        public ICommand SMSCommand
        {
            get
            {
                return new RelayCommand(SMS);
            }
        }
        #endregion

        #region Métodos
        private void Call()
        {
            Plugins.PlacePhoneCall(this.Celular);
        }

        private async void SMS()
        {
            await Plugins.SendSms("Test prueba", new string[] { this.Celular });
        }

        private async void GoItem()
        {
            MainViewModel.GetInstance().Persona = new PersonaViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PersonaPage());
        }
        #endregion
    }
}
