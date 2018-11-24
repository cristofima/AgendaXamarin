using Agenda.Interfaces;
using Agenda.Models;
using Agenda.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PersonasViewModel : BaseViewModel
    {
        #region Servicios

        private SQliteService _sqliteService;

        #endregion Servicios

        #region Atributos

        private ObservableCollection<Persona> personasLista;

        #endregion Atributos

        #region Propiedades

        public ObservableCollection<Persona> PersonasLista
        {
            get { return personasLista; }
            set
            {
                SetValue(ref this.personasLista, value);
            }
        }

        #endregion Propiedades

        public PersonasViewModel()
        {
            _sqliteService = new SQliteService();

            this.PersonasLista = new ObservableCollection<Persona>();

            //this.ResetList();

            this.PersonasLista.Add(new Persona
            {
                Nombres = "Carlos",
                Apellidos = "Ortega",
                Celular = "09181512515",
                DOB = new DateTime(1980, 11, 5)
            });
        }

        private async void ResetList()
        {
            var result = await _sqliteService.GetPersonasAsync();

            this.PersonasLista.Clear();

            foreach (var item in result)
            {
                PersonasLista.Add(item);
            }
        }

        #region Comandos

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(GoPersona);
            }
        }

        private async void GoPersona()
        {
            MainViewModel.GetInstance().Persona = new PersonaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PersonaPage());
        }

        #endregion Comandos
    }
}