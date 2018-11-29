using Agenda.Helpers;
using Agenda.Interfaces;
using Agenda.Models;
using Agenda.Validators;
using GalaSoft.MvvmLight.Command;
using Plugin.ValidationRules;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PersonaViewModel : BaseViewModel
    {
        #region Validaciones

        private ValidationUnit ValidationUnit;
        public ValidatableObject<string> NombresRule { get; set; }
        public ValidatableObject<string> ApellidosRule { get; set; }
        public ValidatableObject<string> CelularRule { get; set; }

        private void InitValidations()
        {
            this.NombresRule = new ValidatableObject<string>();
            this.ApellidosRule = new ValidatableObject<string>();
            this.CelularRule = new ValidatableObject<string>();

            this.ValidationUnit = new ValidationUnit(NombresRule, ApellidosRule, CelularRule);

            NombresRule.Validations.Add(new IsNullOrEmpty<string> { ValidationMessage = "Los nombres son obligatorios." });
            NombresRule.Validations.Add(new PersonNames<string> { ValidationMessage = "Los nombres no coinciden con el formato." });

            ApellidosRule.Validations.Add(new IsNullOrEmpty<string> { ValidationMessage = "Los apellidos son obligatorios." });
            ApellidosRule.Validations.Add(new PersonNames<string> { ValidationMessage = "Los apellidos no coinciden con el formato." });

            CelularRule.Validations.Add(new IsNullOrEmpty<string> { ValidationMessage = "El celular es obligatorio." });
            CelularRule.Validations.Add(new Cellphone<string> { ValidationMessage = "El celular no coincide con el formato." });
        }

        private bool ValidateForm()
        {
            return this.ValidationUnit.Validate();
        }

        #endregion Validaciones

        #region Servicios

        private SQliteService _sqliteService;

        #endregion Servicios

        #region Atributos

        private Persona persona;

        #endregion Atributos

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

        #endregion Propiedaes

        #region Constructores

        public PersonaViewModel() : this(null)
        {
        }

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
            this.InitValidations();
            this.SetDataValidate();
            _sqliteService = new SQliteService();

            this.ValidateControlCommand = new Command<string>(ValidateControl);
        }

        #endregion Constructores

        #region Comandos

        public ICommand ValidateControlCommand { get; private set; }

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

        #endregion Comandos

        #region Métodos

        private void ValidateControl(string control)
        {
            switch (control)
            {
                case "Nombres":
                    this.NombresRule.Validate();
                    break;
                case "Apellidos":
                    this.ApellidosRule.Validate();
                    break;
                case "Celular":
                    this.CelularRule.Validate();
                    break;
                default:
                    break;
            }
        }

        private void SetDataValidate()
        {
            this.NombresRule.Value = this.Persona.Nombres;
            this.ApellidosRule.Value = this.Persona.Apellidos;
            this.CelularRule.Value = this.Persona.Celular;
        }

        private void SetDataPersonaFromRules()
        {
            this.Persona.Nombres = this.NombresRule.Value;
            this.Persona.Apellidos = this.ApellidosRule.Value;
            this.Persona.Celular = this.CelularRule.Value;
        }

        private async void Save()
        {
            if (this.ValidateForm())
            {
                this.SetDataPersonaFromRules();
                await _sqliteService.SavePersonaAsync(this.Persona);
                MessagingCenter.Send<string>("App", "ResetList");
                DependencyService.Get<IMessage>().ShortAlert($"Los datos de la persona {this.Persona.Nombres} {this.Persona.Apellidos} fueron " +
                    $"guardados satisfactoriamente.");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Plugins.Vibrate();
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese los datos", "OK");
            }
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

        #endregion Métodos
    }
}