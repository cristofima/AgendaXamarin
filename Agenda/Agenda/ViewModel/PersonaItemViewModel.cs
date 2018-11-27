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

        #region Comandos
        public ICommand SelectCommand
        {
            get
            {
                return new RelayCommand(GoItem);
            }
        }
        #endregion

        #region Métodos
        private async void GoItem()
        {
            MainViewModel.GetInstance().Persona = new PersonaViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PersonaPage());
        }
        #endregion
    }
}
