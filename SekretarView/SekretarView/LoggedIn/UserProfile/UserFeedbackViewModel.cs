using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class UserFeedbackViewModel : ViewModelBase
    {
        private String _feedback;
        private String _thankYouMessage;

        private ICommand _leaveFeedback;

        public ICommand LeaveFeedback
        {
            get
            {
                if (_leaveFeedback == null)
                    _leaveFeedback = new RelayCommand(p => leaveFeedback(), p => Feedback.Length > 0);
                return _leaveFeedback;
            }
        }

        public int Length
        {
            get
            {
                return Feedback.Length;
            }
        }

        public String Feedback
        {
            get
            {
                return _feedback;
            }
            set
            {
                _feedback = value;
                OnPropertyChanged("Feedback");
                OnPropertyChanged("Length");
            }
        }

        public String ThankYou
        {
            get
            {
                return _thankYouMessage;
            }
            set
            {
                _thankYouMessage = value;
                OnPropertyChanged("ThankYou");
            }
        }

        public void leaveFeedback()
        {
            Feedback = "";
            ThankYou = "Hvala Vam što ste ostavili utisak.";
        }

        public UserFeedbackViewModel() : base("Ostavi utisak", true)
        {
            _thankYouMessage = null;
            _feedback = "";
        }
    }
}
