using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    class MenuItemGroupViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItemViewModel> _items;

        public ObservableCollection<MenuItemViewModel> Items
        {
            get
            {
                return _items;
            }
        }

        public MenuItemGroupViewModel(string title) : base(title)
        {
            _items = new ObservableCollection<MenuItemViewModel>();
        }

        public MenuItemGroupViewModel(string title, ObservableCollection<MenuItemViewModel> items) : base(title)
        {
            _items = items;
        }
    }
}
