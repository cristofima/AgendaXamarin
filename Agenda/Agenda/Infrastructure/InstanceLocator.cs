using Agenda.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infrastructure
{
    public class InstanceLocator
    {
        #region Propiedades

        public MainViewModel Main
        {
            get;
            set;
        }

        #endregion Properties

        #region Constructors

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }

        #endregion Constructors
    }
}
