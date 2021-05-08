using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class LogInViewModel : ViewModelBase
    {
        private String _username;
        private String _password;
        private String _error;

        private ICommand _logInCommand;

        private Action<EmployeeAccount> _logInCallback;

        public String Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Username", "Polje je obavezno.");
                else
                    OnErrorChanged("Username", null);

                OnPropertyChanged("Username");
            }
        }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Password", "Polje je obavezno.");
                else
                    OnErrorChanged("Password", null);

                OnPropertyChanged("Password");
            }
        }

        public String Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }


        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null)
                    _logInCommand = new RelayCommand(p => LogIn(), p => canTryLogIn());

                return _logInCommand;
            }
        }

        private Boolean canTryLogIn()
        {
            return Username != null && !Username.Equals("") && Password != null && !Password.Equals("");
        }

        private void LogIn()
        {
            EmployeeAccount employee =
                DataMockup.Instance.Users.Find(account => account.Username.Equals(Username) && account.Password.Equals(Password));

            if (employee == null)
            {
                Error = "Pogrešno korisničko ime ili lozinka. Ispravite unos i pokušajte ponovo.";
                return;
            }

            Error = "";
            _logInCallback(employee);
        }

        public LogInViewModel(Action<EmployeeAccount> logInCallback)
        {
            _logInCallback = logInCallback;
            _error = "";
            _password = "sekretar";
            _username = "sekretar";
        }
    }
}
