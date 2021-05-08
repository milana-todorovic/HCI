using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SekretarView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IEnumerable<Swatch> swatches = new SwatchesProvider().Swatches;
            PaletteHelper ph = new PaletteHelper();
            ITheme theme = ph.GetTheme();
            theme.SecondaryMid = Color.FromRgb(224, 224, 224);
            theme.SecondaryLight = Color.FromRgb(238, 238, 238);
            theme.SecondaryDark = Color.FromRgb(189, 189, 189);
            ph.SetTheme(theme);
            this.DataContext = new ApplicationViewModel();
        }
    }
}
