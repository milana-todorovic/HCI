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
    class PatientSelectionViewModel : PatientListViewModel
    {
        private Action<Patient> _selected;
        private ViewModelBase _caller;

        private ICommand _confirmSelection;
        private ICommand _cancel;

        private PatientDetailsViewModel _selectedPatient;

        public PatientDetailsViewModel SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

        public ICommand ConfirmSelection
        {
            get
            {
                if (_confirmSelection == null)
                    _confirmSelection = new RelayCommand(p => confirmSelection(), p => SelectedPatient != null);
                return _confirmSelection;
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

        public PatientSelectionViewModel(ICommand changeViewCommand, 
            Action<Patient> selected, ViewModelBase caller) : base(changeViewCommand, "Izaberi pacijenta", true)
        {
            _selected = selected;
            _caller = caller;
        }

        private void confirmSelection()
        {
            _selected.Invoke(SelectedPatient.Patient);
            _changeViewCommand.Execute(_caller);
        }

        private void cancel()
        {
            _changeViewCommand.Execute(_caller);
        }

        override protected void loadPatients()
        {
            _allPatients = new ObservableCollection<PatientDetailsViewModel>();
            foreach (Patient patient in DataMockup.Instance.Patients)
                _allPatients.Add(new PatientDetailsViewModel(patient, this, false, _changeViewCommand));
            ClearSearch();
        }
    }
}
