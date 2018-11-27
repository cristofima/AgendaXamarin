using Agenda.Interfaces;
using Agenda.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PersonaViewModel : BaseViewModel
    {
        #region Servicios

        private SQliteService _sqliteService;

        #endregion Servicios

        #region Atributos
        private Persona persona;
        #endregion

        #region Propiedaes
        public bool IsDeleteVisible { get; set; }

        public Persona Persona
        {
            get
            {
                return persona;
            }
            set
            {
                SetValue(ref this.persona, value);
            }
        }
        #endregion

        #region Constructores
        public PersonaViewModel() : this(null) { }

        public PersonaViewModel(Persona per)
        {
            if (per != null)
            {
                this.Persona = new Persona(per);
                this.IsDeleteVisible = true;
            }
            else
            {
                this.Persona = new Persona();
            }
            _sqliteService = new SQliteService();
        }
        #endregion

        #region Comandos
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }
        #endregion

        #region Métodos
        private async void Save()
        {
            await _sqliteService.SavePersonaAsync(this.Persona);
            MessagingCenter.Send<string>("App", "ResetList");
            DependencyService.Get<IMessage>().ShortAlert($"Los datos de la persona {this.Persona.Nombres} {this.Persona.Apellidos} fueron " +
                $"guardados satisfactoriamente.");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void Delete()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Advertencia",
                "Desea eliminar los datos de la persona", "Aceptar", "Cancelar");
            if (result)
            {
                await _sqliteService.DeletePersonaAsync(this.Persona);
                MessagingCenter.Send<string>("App", "ResetList");
                DependencyService.Get<IMessage>().ShortAlert($"Los datos de la persona {this.Persona.Nombres} {this.Persona.Apellidos} fueron " +
                $"eliminados satisfactoriamente.");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        #endregion
    }
}
