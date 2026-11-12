using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Jonatan_YudBetProj.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {
        private string usernameEntry = string.Empty;
        private string usernameErrorMessage = string.Empty;
        private string emailEntry = string.Empty;
        private string passwordEntry = string.Empty;
        private string confirmPasswordEntry = string.Empty;
        private string emailErrorMessage = string.Empty;
        private string passwordErrorMessage = string.Empty;
        private string confirmPasswordErrorMessage = string.Empty;
        private string registerErrorMessage = string.Empty;
        private bool isRegistered;

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterAsync());
            LoginCommand = new Command(async () => await Shell.Current.GoToAsync("LoginPage"));
        }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public string UsernameEntry
        {
            get => usernameEntry;
            set => SetProperty(ref usernameEntry, value ?? string.Empty);
        }
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
        public string ConfirmPasswordEntry
        {
            get => confirmPasswordEntry;
            set => SetProperty(ref confirmPasswordEntry, value ?? string.Empty);
        }
        public string UsernameErrorMessage
        {
            get => usernameErrorMessage;
            private set
            {
                if (SetProperty(ref usernameErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasUsernameError));
                }
            }
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
        public string ConfirmPasswordErrorMessage
        {
            get => confirmPasswordErrorMessage;
            private set
            {
                if (SetProperty(ref confirmPasswordErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasConfirmPasswordError));
                }
            }
        }
        public string RegisterErrorMessage
        {
            get => registerErrorMessage;
            private set
            {
                if (SetProperty(ref registerErrorMessage, value))
                {
                    OnPropertyChanged(nameof(HasRegisterError));
                }
            }
        }
        public bool HasUsernameError => !string.IsNullOrEmpty(UsernameErrorMessage);
        public bool HasEmailError => !string.IsNullOrEmpty(EmailErrorMessage);
        public bool HasPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);
        public bool HasConfirmPasswordError => !string.IsNullOrEmpty(ConfirmPasswordErrorMessage);
        public bool HasRegisterError => !string.IsNullOrEmpty(RegisterErrorMessage);
        public bool IsRegistered
        {
            get => isRegistered;
            private set => SetProperty(ref isRegistered, value);
        }
        Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        private async Task RegisterAsync()
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(UsernameEntry))
            {
                UsernameErrorMessage = "Username is required.";
            }
            if (!string.IsNullOrWhiteSpace(UsernameEntry) && UsernameEntry.Length < 3)
            {
                UsernameErrorMessage = "Username must be at least 3 characters long.";
            }
            if (string.IsNullOrWhiteSpace(EmailEntry))
            {
                EmailErrorMessage = "Email is required.";
            }
            else if (!emailRegex.IsMatch(EmailEntry))
            {
                EmailErrorMessage = "Please enter a valid email address.";
            }
            if (string.IsNullOrWhiteSpace(PasswordEntry))
            {
                PasswordErrorMessage = "Password is required.";
            }
            else if (!passwordRegex.IsMatch(PasswordEntry))
            {
                PasswordErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.";
            }
            if (!string.IsNullOrWhiteSpace(PasswordEntry) && PasswordEntry != ConfirmPasswordEntry)
            {
                ConfirmPasswordErrorMessage = "Passwords do not match.";
            }
            if (HasUsernameError || HasEmailError || HasPasswordError || HasConfirmPasswordError)
            {
                return;
            }
            if (services.Database.UserExists(UsernameEntry))
            {
                RegisterErrorMessage = "Username is already taken.";
                return;
            }
            if (services.Database.EmailExists(EmailEntry))
            {
                RegisterErrorMessage = "Email is already taken.";
                return;
            }
            else
            {
                var newUser = new Models.User
                {
                    Id = services.Database.UsersList.Count + 1,
                    Name = usernameEntry,
                    Email = emailEntry,
                    Password = passwordEntry,
                    ImageSRC = "default_img.jpg"
                };
                services.Database.AddUser(newUser);
                IsRegistered = true;
                await Shell.Current.GoToAsync("LoginPage");
            }
        }
        private void ClearErrors()
        {
            UsernameErrorMessage = string.Empty;
            EmailErrorMessage = string.Empty;
            PasswordErrorMessage = string.Empty;
            ConfirmPasswordErrorMessage = string.Empty;
            RegisterErrorMessage = string.Empty;
        }
    }
}
