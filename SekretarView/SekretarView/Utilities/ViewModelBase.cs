using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        protected string _name;
        protected string _globalError;
        protected Boolean _temporary;

        public string Title { get { return _name; } }
        public Boolean Temporary { get { return _temporary; } }

        private Dictionary<String, String> _errors = new Dictionary<string, string>();

        public bool HasErrors => _errors.Count > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected virtual void OnErrorChanged(string name, string error)
        {
            if (error == null || error.Equals(""))
                _errors.Remove(name);
            else
                _errors[name] = error;

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(name));
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return new string[1] { _errors[propertyName] };
            else
                return null;
        }

        protected ViewModelBase(string name)
        {
            _name = name;
            _temporary = false;
        }

        protected ViewModelBase(string name, Boolean temporary)
        {
            _name = name;
            _temporary = temporary;
        }

        protected ViewModelBase()
        {
            _temporary = false;
        }
    }
}
