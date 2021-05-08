using Model.Users.Generalities;
using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class EditProfileViewModel : ViewModelBase
    {
        private ICommand _changeView;
        private ICommand _saveChanges;
        private ICommand _cancel;
        private ViewModelBase _userProfile;

        private EmployeeAccount _secretary;
        private String _phoneNumber;
        private String _street;
        private City _city;

        public ICommand Cancel
        {
            get
            {
                if(_cancel == null)
                {
                    _cancel = new RelayCommand(p => cancel());
                }

                return _cancel;
            }
        }

        public ICommand SaveChanges
        {
            get
            {
                if (_saveChanges == null)
                {
                    _saveChanges = new RelayCommand(p => saveChanges(), p => canSaveChanges());
                }

                return _saveChanges;
            }
        }

        public ObservableCollection<City> Cities
        {
            get; private set;
        }

        public ViewModelBase UserProfile
        {
            get
            {
                return _userProfile;
            }
        }

        public City City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;

                if (value == null)
                    OnErrorChanged("City", "Mora biti izabran jedan od ponuđenh gradova.");
                else
                    OnErrorChanged("City", null);

                OnPropertyChanged("City");
            }
        }

        public String Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Street", "Polje je obavezno.");
                else
                    OnErrorChanged("Street", null);

                OnPropertyChanged("Street");
            }
        }

        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("PhoneNumber", "Polje je obavezno.");
                else if (!value.All(c => (c >= '0' && c <= '9') || c == '-' || c == '/'))
                    OnErrorChanged("PhoneNumber", "Broj telefona sme sadržati cifre od 0 do 9, i karaktere / i -.");
                else
                    OnErrorChanged("PhoneNumber", "");

                OnPropertyChanged("PhoneNumber");
            }
        }

        public EditProfileViewModel(ICommand changeView, ViewModelBase userProfile, EmployeeAccount secretary) : base("Izmeni profil", true)
        {
            _changeView = changeView;
            _userProfile = userProfile;
            _secretary = secretary;
            _phoneNumber = secretary.Employee.TelephoneNumber;
            _city = secretary.Employee.Address.City;
            _street = secretary.Employee.Address.Street;
            Cities = new ObservableCollection<City>(DataMockup.Instance.Cities);
        }

        private Boolean canSaveChanges()
        {
            return !this.HasErrors;
        }

        private void saveChanges()
        {
            _secretary.Employee.TelephoneNumber = PhoneNumber;
            _secretary.Employee.Address.City = City;
            _secretary.Employee.Address.Street = Street;

            _changeView.Execute(UserProfile);
        }

        private void cancel()
        {
            PhoneNumber = _secretary.Employee.TelephoneNumber;
            Street = _secretary.Employee.Address.Street;
            City = _secretary.Employee.Address.City;

            _changeView.Execute(UserProfile);
        }
    }
}
