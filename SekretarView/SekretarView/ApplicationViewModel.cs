using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class ApplicationViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;

        private Boolean _loggedIn;

        private ICommand _logOutCommand;
        private ICommand _navigateBackCommand;

        public ApplicationViewModel() : base("Aplikacija za sekretara")
        {
            _loggedIn = false;
            CurrentView = new LogInViewModel(this.SuccessfullyLoggedIn);
        }

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

        public ICommand LogOutCommand
        {
            get
            {
                if (_logOutCommand == null)
                {
                    _logOutCommand = new RelayCommand(param => LogOut());
                }

                return _logOutCommand;
            }
        }

        public ICommand NavigateBackCommand
        {
            get
            {
                if (_navigateBackCommand is null)
                {
                    _navigateBackCommand = new RelayCommand(p => NavigateBack(), p => CanNavigateBack());
                }

                return _navigateBackCommand;
            }
        }

        private void SuccessfullyLoggedIn(EmployeeAccount account)
        {
            _loggedIn = true;
            this.CurrentView = new MainContainerViewModel(LogOutCommand, account);
        }

        private void LogOut()
        {
            _loggedIn = false;
            this.CurrentView = new LogInViewModel(this.SuccessfullyLoggedIn);
        }

        private Boolean CanNavigateBack()
        {
            if (!_loggedIn) return false;
            return ((MainContainerViewModel)CurrentView).NavigateBackCommand.CanExecute(null);
        }

        private void NavigateBack()
        {
            ((MainContainerViewModel)CurrentView).NavigateBackCommand.Execute(null);
        }

    }
}
