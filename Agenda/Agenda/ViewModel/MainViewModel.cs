using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region ViewModels
        public PersonasViewModel PersonasLista
        {
            get;
            set;
        }

        public PersonaViewModel Persona
        {
            get;
            set;
        }
        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;
            this.PersonasLista = new PersonasViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
