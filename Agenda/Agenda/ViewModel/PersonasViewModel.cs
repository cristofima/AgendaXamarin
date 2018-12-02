using Agenda.Interfaces;
using Agenda.Models;
using Agenda.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool isRefreshing;

        #endregion Atributos

        #region Propiedades

        private List<Persona> PeopleList { get; set; }

        private bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                SetValue(ref isRefreshing, value);
            }
        }

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

            this.GoPreferencesCommand = new Command(GoPreferences);
            this.AddCommand = new Command(GoPersona);
            this.SearchCommand = new Command(FilterList);
            this.RefreshCommand = new Command(ResetList);
        }

        #region Metodos

        private async void ResetList()
        {
            this.IsRefreshing = true;
            this.PeopleList = await _sqliteService.GetPersonasAsync();

            this.PersonasLista.Clear();

            foreach (var item in PeopleList)
            {
                PersonasLista.Add(new PersonaItemViewModel(item));
            }

            this.IsRefreshing = false;
        }

        private IEnumerable<PersonaItemViewModel> ToPersonaItemViewModel()
        {
            return this.PeopleList.Select(p => new PersonaItemViewModel
            {
                Id = p.Id,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Celular = p.Celular,
                DOB = p.DOB
            });
        }

        private void FilterList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.PersonasLista = new ObservableCollection<PersonaItemViewModel>(this.ToPersonaItemViewModel());
            }
            else
            {
                string auxFilter = this.Filter.ToLower().Trim();
                if (!string.IsNullOrWhiteSpace(auxFilter))
                {
                    this.PersonasLista = new ObservableCollection<PersonaItemViewModel>(
                            this.ToPersonaItemViewModel().Where(p => p.Nombres.ToLower().Contains(auxFilter) ||
                                p.Apellidos.ToLower().Contains(auxFilter)
                        )
                      );
                }
            }
        }

        #endregion Metodos

        #region Comandos

        public ICommand GoPreferencesCommand { get; private set; }

        public ICommand AddCommand { get; private set; }


        public ICommand SearchCommand { get; private set; }

        public ICommand RefreshCommand { get; private set; }

        private async void GoPersona()
        {
            MainViewModel.GetInstance().Persona = new PersonaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PersonaPage());
        }

        private async void GoPreferences()
        {
            MainViewModel.GetInstance().Preferences = new PreferencesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PreferencesPage());
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

        #endregion Eventos
    }
}