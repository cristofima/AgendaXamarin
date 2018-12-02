using Agenda.Helpers;
using Agenda.Models;
using Agenda.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class PersonaItemViewModel : Persona
    {
        public PersonaItemViewModel(Persona per) : base(per)
        {
            this.InitCommand();
        }

        public PersonaItemViewModel()
        {
            this.InitCommand();
        }

        private void InitCommand()
        {
            this.SelectCommand = new Command(GoItem);
            this.CallCommand = new Command(Call);
            this.SMSCommand = new Command(SMS);
        }

        #region Comandos

        public ICommand SelectCommand { get; private set; }

        public ICommand CallCommand { get; private set; }

        public ICommand SMSCommand { get; private set; }

        #endregion Comandos

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

        #endregion Métodos
    }
}