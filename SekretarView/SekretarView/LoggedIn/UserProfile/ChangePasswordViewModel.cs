using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class ChangePasswordViewModel : ViewModelBase
    {
        private ICommand _changeView;
        private ICommand _savePassword;
        private ICommand _cancel;
        private ViewModelBase _userProfile;
        private EmployeeAccount _secretary;

        private String _currentPasswordInput;
        private String _newPasswordInput;
        private String _newPasswordRepeated;

        private Boolean _passwordMismatch;

        public String CurrentPasswordInput
        {
            get
            {
                return _currentPasswordInput;
            }
            set
            {
                _currentPasswordInput = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("CurrentPasswordInput", "Polje je obavezno.");
                else if (!value.Equals(_secretary.Password))
                    OnErrorChanged("CurrentPasswordInput", "Unos se ne poklapa sa trenutnom lozinkom.");
                else
                    OnErrorChanged("CurrentPasswordInput", "");

                OnPropertyChanged("CurrentPasswordInput");
            }
        }

        public String NewPasswordInput
        {
            get
            {
                return _newPasswordInput;
            }
            set
            {
                _newPasswordInput = value;

                if (value == null || value.Equals(""))
                {
                    OnErrorChanged("NewPasswordInput", "Polje je obavezno.");
                    if (_passwordMismatch)
                    {
                        _passwordMismatch = false;
                        OnErrorChanged("NewPasswordRepeated", null);
                    }
                }
                else if (NewPasswordRepeated != null && !NewPasswordRepeated.Equals("") && !NewPasswordRepeated.Equals(value))
                {
                    OnErrorChanged("NewPasswordRepeated", "Unosi nove lozinke se ne poklapaju.");
                    _passwordMismatch = true;
                }
                else
                {
                    OnErrorChanged("NewPasswordInput", null);

                    if (_passwordMismatch)
                    {
                        _passwordMismatch = false;
                        OnErrorChanged("NewPasswordRepeated", null);
                    }
                }


                OnPropertyChanged("NewPasswordInput");
            }
        }

        public String NewPasswordRepeated
        {
            get
            {
                return _newPasswordRepeated;
            }
            set
            {
                _newPasswordRepeated = value;

                if (value == null || value.Equals(""))
                {
                    OnErrorChanged("NewPasswordRepeated", "Polje je obavezno.");
                    _passwordMismatch = false;
                }
                else if (NewPasswordInput != null && !NewPasswordInput.Equals("") && !NewPasswordInput.Equals(value))
                {
                    OnErrorChanged("NewPasswordRepeated", "Unosi nove lozinke se ne poklapaju.");
                    _passwordMismatch = true;
                }
                else
                {
                    OnErrorChanged("NewPasswordRepeated", null);
                    _passwordMismatch = false;
                }


                OnPropertyChanged("NewPasswordRepeated");
            }
        }

        public ICommand SavePassword
        {
            get
            {
                if (_savePassword == null)
                    _savePassword = new RelayCommand(p => savePassword(), p => canSavePassword());

                return _savePassword;
            }
        }

        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                    _cancel = new RelayCommand(p => cancel());

                return _cancel;
            }
        }

        public ChangePasswordViewModel(ICommand changeView, ViewModelBase userProfile, EmployeeAccount account) : base("Izmeni lozinku", true)
        {
            _secretary = account;
            _newPasswordInput = "";
            _currentPasswordInput = "";
            _newPasswordRepeated = "";
            _passwordMismatch = false;
            _changeView = changeView;
            _userProfile = userProfile;
        }

        public Boolean canSavePassword()
        {
            if (HasErrors)
                return false;
            if (CurrentPasswordInput == null || CurrentPasswordInput.Equals(""))
                return false;
            if (NewPasswordInput == null || NewPasswordInput.Equals(""))
                return false;
            if (NewPasswordRepeated == null || NewPasswordRepeated.Equals(""))
                return false;
            if (!CurrentPasswordInput.Equals(_secretary.Password))
                return false;
            if (!NewPasswordInput.Equals(NewPasswordRepeated))
                return false;

            return true;
        }

        public void savePassword()
        {
            _secretary.Password = NewPasswordInput;

            CurrentPasswordInput = "";
            NewPasswordInput = "";
            NewPasswordRepeated = "";
            _changeView.Execute(_userProfile);
            _passwordMismatch = false;

            _changeView.Execute(_userProfile);
        }

        public void cancel()
        {
            CurrentPasswordInput = "";
            NewPasswordInput = "";
            NewPasswordRepeated = "";
            _changeView.Execute(_userProfile);
            _passwordMismatch = false;
        }
    }
}
