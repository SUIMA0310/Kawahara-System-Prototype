using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsClientApplication.ViewModels {
    public class ShellViewModel : BindableBase {

        private string _title = "Windows Client Application";
        public string Title {
            get { return _title; }
            set { SetProperty( ref _title, value ); }
        }

    }
}
