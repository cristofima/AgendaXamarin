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

        private ObservableCollection<PersonaItemViewModel> personasLista;
        private string filter;

        #endregion Atributos

        #region Propiedades
        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                SetValue(ref this.filter, value);
            }
        }

        public ObservableCollection<PersonaItemViewModel> PersonasLista
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

            this.PersonasLista = new ObservableCollection<PersonaItemViewModel>();

            this.ResetList();
            this.InitEventos();
        }

        #region Metodos
        private async void ResetList()
        {
            var result = await _sqliteService.GetPersonasAsync();

            this.PersonasLista.Clear();

            foreach (var item in result)
            {
                PersonasLista.Add(new PersonaItemViewModel(item));
            }
        } 

        private void FilterList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                
            }
            else
            {
                string auxFilter = this.Filter.ToLower().Trim();
            }
        }
        #endregion

        #region Comandos

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(GoPersona);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(FilterList);
            }
        }

        private async void GoPersona()
        {
            MainViewModel.GetInstance().Persona = new PersonaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PersonaPage());
        }

        #endregion Comandos

        #region Eventos
        private void InitEventos()
        {
            MessagingCenter.Subscribe<string>("App", "ResetList", (sender) =>
            {
                this.ResetList();
            });
        }
        #endregion


    }
}