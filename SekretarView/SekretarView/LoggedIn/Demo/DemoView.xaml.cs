using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for DemoView.xaml
    /// </summary>
    public partial class DemoView : UserControl
    {
        public DemoView()
        {
            InitializeComponent();
            Holder.Content = new DemoDescriptionView();
        }

        private async void RunDemo(object sender, RoutedEventArgs e)
        {

            await DemoMode();

        }

        private StepOneDemoDummy stepOne;
        private ChoosePatientAndTypeViewModel stepOneContext;
        private PatientSelectionDemoDummy patientSelection;
        private PatientSelectionViewModel patientSelectionContext;
        private StepTwoDemoDummy stepTwo;
        private ChooseSchedulingInfoViewModel stepTwoContext;
        private StepThreeDummy stepThree;
        private ChooseProcedureViewModel stepThreeContext;

        public async Task DemoMode()
        {
            stepOne = new StepOneDemoDummy();
            stepOneContext = new ChoosePatientAndTypeViewModel("", null, null, true);
            stepOne.DataContext = stepOneContext;
            patientSelection = new PatientSelectionDemoDummy();
            patientSelectionContext = new PatientSelectionViewModel(null, null, null);
            patientSelection.DataContext = patientSelectionContext;

            Holder.Content = stepOne;
            await Task.Delay(1100);

            stepOne.Tip.Focus();
            await Task.Delay(900);
            stepOne.Tip.IsDropDownOpen = true;
            await Task.Delay(1100);
            stepOne.Tip.SelectedIndex = 1;
            await Task.Delay(900);
            stepOne.Tip.IsDropDownOpen = false;

            await Task.Delay(1100);
            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(stepOne.IzaberiPacijenta, new object[0]);
            await Task.Delay(700);

            Holder.Content = patientSelection;
            await Task.Delay(1100);
            patientSelection.ListaPacijenata.Focus();
            patientSelection.ListaPacijenata.SelectedIndex = 0;
            stepOneContext.patientSelected(patientSelectionContext.Patients[0].Patient);
            await Task.Delay(1500);
            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(patientSelection.PotvrdiIzbor, new object[0]);
            await Task.Delay(900);

            Holder.Content = stepOne;
            await Task.Delay(1600);
            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(stepOne.SledeciKorak, new object[0]);
            await Task.Delay(900);

            stepTwo = new StepTwoDemoDummy();
            stepTwoContext = new ChooseSchedulingInfoViewModel(null, stepOneContext.Procedure, null, null, null);
            stepTwo.DataContext = stepTwoContext;
            Holder.Content = stepTwo;
            await Task.Delay(1100);

            stepTwoContext.ExactDate = true;
            await Task.Delay(1100);

            stepTwo.Datum.Focus();
            await Task.Delay(900);
            stepTwo.Datum.IsDropDownOpen = true;
            await Task.Delay(1100);
            stepTwo.Datum.SelectedDate = DateTime.Now.Date.AddDays(-3);
            await Task.Delay(900);
            stepTwo.Datum.IsDropDownOpen = false;
            await Task.Delay(1400);

            stepTwo.Datum.Focus();
            await Task.Delay(900);
            stepTwo.Datum.IsDropDownOpen = true;
            await Task.Delay(1100);
            stepTwo.Datum.SelectedDate = DateTime.Now.Date.AddDays(2);
            await Task.Delay(900);
            stepTwo.Datum.IsDropDownOpen = false;
            await Task.Delay(1600);

            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(stepTwo.SledeciKorak, new object[0]);
            await Task.Delay(900);

            stepThree = new StepThreeDummy();
            stepThreeContext = new ChooseProcedureViewModel(null, null, null, stepTwoContext.Procedure, null);
            stepThree.DataContext = stepThreeContext;
            Holder.Content = stepThree;
            await Task.Delay(1800);
            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(stepThree.Zakazi, new object[0]);
            await Task.Delay(900);

            Zapocni.Content = "Ponovi demonstraciju";
            Nazad.Content = "Nazad";
            if (DataContext != null)
            {
                ((DemoViewModel)DataContext).DemoActive = false;
                Holder.Content = new DemoFinished();
            }

        }

    }
}
