using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Jonatan_YudBetProj.ViewModels
{
    internal class WelcomePageViewModel : ViewModelBase
    {
        public WelcomePageViewModel()
        {
            LoginCommand = new Command(async () => await Shell.Current.GoToAsync("LoginPage"));
            RegisterCommand = new Command(async () => await Shell.Current.GoToAsync("RegisterPage"));
        }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
    }   
}
