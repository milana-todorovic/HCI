using Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class PatientListViewModel : ViewModelBase
    {
        protected ObservableCollection<PatientDetailsViewModel> _allPatients;
        protected ObservableCollection<PatientDetailsViewModel> _showingPatients;
        protected Patient _filter;

        protected ICommand _changeViewCommand;
        private ICommand _addUnregistered;
        private ICommand _addRegistered;
        private ICommand _showDetails;
        protected ICommand _filterView;
        private ICommand _clearFilter;

        public ICommand ClearFilter
        {
            get
            {
                if (_clearFilter == null)
                    _clearFilter = new RelayCommand(p => ClearSearch());
                return _clearFilter;
            }
        }

        public ICommand Filter
        {
            get
            {
                if (_filterView == null)
                    _filterView = new RelayCommand(p => openFilter());
                return _filterView;
            }
        }

        private void openFilter()
        {
            _changeViewCommand.Execute(new PatientFilterViewModel(this, FilterPatients, _changeViewCommand, _filter));
        }

        #region Properties
        public ObservableCollection<PatientDetailsViewModel> Patients
        {
            get
            {
                return _showingPatients;
            }
            set
            {
                _showingPatients = value;
                OnPropertyChanged("Patients");
            }
        }

        public ICommand Register
        {
            get
            {
                if (_addRegistered == null)
                    _addRegistered = new RelayCommand(p => _changeViewCommand.Execute(new RegisterPatientViewModel(_changeViewCommand, this)));
                return _addRegistered;
            }
        }
        
        public ICommand ShowDetails
        {
            get
            {
                if (_showDetails == null)
                {
                    _showDetails = new RelayCommand(p => showDetails((PatientDetailsViewModel)p));
                }

                return _showDetails;
            }
        }

        public ICommand AddUnregisteredPatientCommand
        {
            get
            {
                if (_addUnregistered == null)
                {
                    _addUnregistered = new RelayCommand(param => AddUnregistered());
                }

                return _addUnregistered;
            }
        }

        #endregion

        private void AddUnregistered()
        {
            _changeViewCommand.Execute(new AddGuestAccountViewModel(_changeViewCommand, this));
        }

        protected void FilterPatients(String name, String surname, String jmbg)
        {
            _filter.Name = name;
            _filter.Surname = surname;
            _filter.JMBG = jmbg;
            var filteredPatients = _allPatients.Where(patient => patient.matches(_filter));
            Patients = new ObservableCollection<PatientDetailsViewModel>(filteredPatients);
        }

        protected void ClearSearch()
        {
            _filter.Name = "";
            _filter.Surname = "";
            _filter.JMBG = "";
            _showingPatients = _allPatients;
        }

        public PatientListViewModel(ICommand changeViewCommand) : base("Pacijenti")
        {
            _changeViewCommand = changeViewCommand;

            _filter = new Patient();

            loadPatients();

            Mediator.Register("PatientAdded", (p) => loadPatients());
            Mediator.Register("PatientUpdated", (p) => loadPatients());
        }

        public PatientListViewModel(ICommand changeViewCommand, String title, Boolean temporary) : base(title, temporary)
        {
            _changeViewCommand = changeViewCommand;
            _filter = new Patient();
            loadPatients();
        }

        protected virtual void loadPatients()
        {
            _allPatients = new ObservableCollection<PatientDetailsViewModel>();
            foreach (Patient patient in DataMockup.Instance.Patients)
                _allPatients.Add(new PatientDetailsViewModel(patient, this, true, _changeViewCommand));
            ClearSearch();
        }

        private void showDetails(PatientDetailsViewModel p)
        {
            _changeViewCommand.Execute(p);
        }
    }
}
