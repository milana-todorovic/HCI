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
    class MainContainerViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<ViewModelBase> _menuItems;
        private ObservableCollection<ViewModelBase> _quickNavItems;
        private ObservableCollection<ViewModelBase> _popupItems;

        private List<ViewModelBase> _viewHistory;

        private ViewModelBase _currentView;
        private ICommand _changeViewCommand;
        private ICommand _navigateBack;
        #endregion

        public MainContainerViewModel(ICommand logOutCommand, EmployeeAccount account)
        {
            _viewHistory = new List<ViewModelBase>();
            InitialiseNavigations(logOutCommand, account);
        }

        #region Properties
        public ViewModelBase CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                if (value != _currentView)
                {
                    _currentView = value;
                    OnPropertyChanged("CurrentView");
                }
            }
        }

        public ObservableCollection<ViewModelBase> MenuItems
        {
            get
            {
                if (this._menuItems is null)
                {
                    this._menuItems = new ObservableCollection<ViewModelBase>();
                }
                return _menuItems;
            }
        }

        public ObservableCollection<ViewModelBase> QuickNavItems
        {
            get
            {
                if (this._quickNavItems is null)
                {
                    this._quickNavItems = new ObservableCollection<ViewModelBase>();
                }
                return _quickNavItems;
            }
        }

        public ObservableCollection<ViewModelBase> PopupItems
        {
            get
            {
                if (this._popupItems is null)
                {
                    this._popupItems = new ObservableCollection<ViewModelBase>();
                }
                return _popupItems;
            }
        }

        public ICommand ChangeViewCommand
        {
            get
            {
                if (this._changeViewCommand is null)
                {
                    this._changeViewCommand = new RelayCommand(p => ChangeCurrentView((ViewModelBase)p));
                }

                return this._changeViewCommand;
            }
        }

        public ICommand NavigateBackCommand
        {
            get
            {
                if (_navigateBack is null)
                {
                    _navigateBack = new RelayCommand(p => NavigateBack(), p => _viewHistory.Count > 0);
                }

                return _navigateBack;
            }
        }
        #endregion

        #region Methods
        private void ChangeCurrentView(ViewModelBase newView)
        {
            if (CurrentView != null && CurrentView != newView && !CurrentView.Temporary)
                _viewHistory.Add(CurrentView);
            CurrentView = newView;
        }

        private void NavigateBack()
        {
            int index = _viewHistory.Count - 1;
            ViewModelBase switchTo = _viewHistory[index];
            _viewHistory.RemoveAt(index);
            CurrentView = switchTo;
        }

        private void InitialiseNavigations(ICommand logOutCommand, EmployeeAccount account)
        {
            var addGroup = new MenuItemGroupViewModel("Zakaži");
            var newExamination = new MenuItemViewModel("Zakaži pregled", 
                new RelayCommand(p=>ChangeCurrentView(new ChoosePatientAndTypeViewModel("Zakaži pregled", CurrentView, ChangeViewCommand, true))), null);
            var newSurgery = new MenuItemViewModel("Zakaži operaciju",
                new RelayCommand(p => ChangeCurrentView(new ChoosePatientAndTypeViewModel("Zakaži operaciju", CurrentView, ChangeViewCommand, false))), null);
            addGroup.Items.Add(newExamination);
            addGroup.Items.Add(newSurgery);
            addGroup.Items.Add(new MenuItemViewModel("Zakaži bolničko lečenje",
                new RelayCommand(p => ChangeCurrentView(new HospitalizationTypeAndPatientViewModel(CurrentView, ChangeViewCommand))), null));

            var scheduleGroup = new MenuItemGroupViewModel("Raspored");
            List<MenuItemViewModel> blah = new List<MenuItemViewModel>();
            blah.Add(newExamination);
            var examinationSchedule = new DailyProcedureScheduleViewModel(ChangeViewCommand, "Pregledi", new ObservableCollection<MenuItemViewModel>(blah), DataMockup.Instance.Examinations, null);
            scheduleGroup.Items.Add(new MenuItemViewModel("Pregledi", ChangeViewCommand, examinationSchedule));
            blah.Add(newSurgery);
            var surgeryAndExamSchedule = new DailyProcedureScheduleViewModel(ChangeViewCommand, "Pregledi i operacije", new ObservableCollection<MenuItemViewModel>(blah), DataMockup.Instance.Examinations, DataMockup.Instance.Surgeries);
            scheduleGroup.Items.Add(new MenuItemViewModel("Pregledi i operacije", ChangeViewCommand, surgeryAndExamSchedule));
            blah.Remove(newExamination);
            var surgerySchedule = new DailyProcedureScheduleViewModel(ChangeViewCommand, "Operacije", new ObservableCollection<MenuItemViewModel>(blah), null, DataMockup.Instance.Surgeries);
            scheduleGroup.Items.Add(new MenuItemViewModel("Operacije", ChangeViewCommand, surgerySchedule));
            var hospitalizationSchedule = new MenuItemViewModel("Bolnička lečenja", ChangeViewCommand, new DailyHospitalizationScheduleViewModel(_changeViewCommand));
            scheduleGroup.Items.Add(hospitalizationSchedule);
            this.MenuItems.Add(scheduleGroup);

            this.MenuItems.Add(addGroup);

            var patients = new MenuItemViewModel("Pacijenti", ChangeViewCommand, new PatientListViewModel(ChangeViewCommand));
            this.MenuItems.Add(patients);
            this.MenuItems.Add(new MenuItemViewModel("Nedeljni izveštaj", new RelayCommand(p=> new GenerateReportViewModel().generateReport()), null));

            this.QuickNavItems.Add(new MenuItemViewModel("Operacije", ChangeViewCommand, surgerySchedule));
            this.QuickNavItems.Add(new MenuItemViewModel("Pregledi", ChangeViewCommand, examinationSchedule));
            this.QuickNavItems.Add(hospitalizationSchedule);

            this.PopupItems.Add(new MenuItemViewModel("Moj profil", ChangeViewCommand, new UserProfileViewModel(ChangeViewCommand, account)));
            this.PopupItems.Add(new MenuItemViewModel("Ostavi utisak", ChangeViewCommand, new UserFeedbackViewModel()));
            this.PopupItems.Add(new MenuItemViewModel("Odjava", logOutCommand, null));

            ChangeCurrentView(examinationSchedule);
        }
        #endregion
    }
}
