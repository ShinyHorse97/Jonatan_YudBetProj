using Jonatan_YudBetProj.services;
using System.Windows.Input;

namespace Jonatan_YudBetProj.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string emailEntry = string.Empty;
        private string passwordEntry = string.Empty;
        private string emailErrorMessage = string.Empty;
        private string passwordErrorMessage = string.Empty;
        private string loginErrorMessage = string.Empty;
        private bool isLoggedIn;

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await Shell.Current.GoToAsync("RegisterPage"));
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public string EmailEntry
        {
            get => emailEntry;
            set => SetProperty(ref emailEntry, value ?? string.Empty);
        }

        public string PasswordEntry
        {
            get => passwordEntry;
            set => SetProperty(ref passwordEntry, value ?? string.Empty);
        }

        public string EmailErrorMessage
        {
            get => emailErrorMessage;
            private set
            {
                if (SetProperty(ref emailErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasEmailError));
                }
            }
        }

        public string PasswordErrorMessage
        {
            get => passwordErrorMessage;
            private set
            {
                if (SetProperty(ref passwordErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasPasswordError));
                }
            }
        }

        public string LoginErrorMessage
        {
            get => loginErrorMessage;
            private set
            {
                if (SetProperty(ref loginErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasLoginError));
                }
            }
        }

        public bool HasEmailError => !string.IsNullOrEmpty(EmailErrorMessage);
        public bool HasPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);
        public bool HasLoginError => !string.IsNullOrEmpty(LoginErrorMessage);

        public bool IsLoggedIn
        {
            get => isLoggedIn;
            private set => SetProperty(ref isLoggedIn, value);
        }

        private async Task LoginAsync()
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(EmailEntry))
            {
                EmailErrorMessage = "Email is required.";
            }

            if (string.IsNullOrWhiteSpace(PasswordEntry))
            {
                PasswordErrorMessage = "Password is required.";
            }

            if (HasEmailError || HasPasswordError)
            {
                return;
            }

            if (!Database.ValidateUser(EmailEntry, PasswordEntry))
            {
                LoginErrorMessage = "The email or password is incorrect.";
                return;
            }

            IsLoggedIn = true;
            await Shell.Current.GoToAsync("FirstPage");
        }

        private void ClearErrors()
        {
            EmailErrorMessage = string.Empty;
            PasswordErrorMessage = string.Empty;
            LoginErrorMessage = string.Empty;
        }
    }
}
