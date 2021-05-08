using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class UserProfileViewModel : ViewModelBase
    {
        private ICommand _changeViewCommand;
        private ViewModelBase _changePassword;
        private ViewModelBase _editProfile;

        private EmployeeAccount _secretary;

        public ICommand ChangeViewCommand
        {
            get
            {
                return _changeViewCommand;
            }
        }

        public ViewModelBase ChangePassword
        {
            get
            {
                return _changePassword;
            }
        }

        public ViewModelBase EditProfile
        {
            get
            {
                return _editProfile;
            }
        }

        public String NameAndSurname
        {
            get
            {
                return _secretary.Employee.Name + " " + _secretary.Employee.Surname;
            }
        }

        public String Username
        {
            get
            {
                return _secretary.Username;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _secretary.Employee.DateOfBirth;
            }
        }

        public String Address
        {
            get
            {
                return _secretary.Employee.Address.ToString();
            }
        }

        public String PhoneNumber
        {
            get
            {
                return _secretary.Employee.TelephoneNumber;
            }
        }

        public String JMBG
        {
            get
            {
                return _secretary.Employee.JMBG;
            }
        }

        public UserProfileViewModel(ICommand changeViewCommand, EmployeeAccount account) : base("Moj profil")
        {
            _secretary = account;
            _changeViewCommand = changeViewCommand;
            _changePassword = new ChangePasswordViewModel(changeViewCommand, this, _secretary);
            _editProfile = new EditProfileViewModel(changeViewCommand, this, _secretary);
        }

    }
}
