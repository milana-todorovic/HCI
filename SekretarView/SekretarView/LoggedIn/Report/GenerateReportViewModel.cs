using Model.HospitalResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Syncfusion.Pdf;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows.Documents;
using System.Windows;
using Model.Schedule.Procedures;

namespace SekretarView
{
    class GenerateReportViewModel : ViewModelBase
    {

        private List<DateTime> getWeek()
        {
            List<DateTime> retVal = new List<DateTime>();

            DateTime monday = DateTime.Now.Date;
            int offset = Convert.ToInt32(monday.DayOfWeek.ToString("D"));
            if (offset != 1) monday = monday.AddDays(-offset);

            for (int i = 0; i < 7; i++)
                retVal.Add(monday.AddDays(i));

            return retVal;
        }

        private ObservableCollection<ReportProcedure> _examinations;
        private ObservableCollection<ReportProcedure> _surgeries;

        public ObservableCollection<ReportProcedure> Examinations { get { return _examinations; } }
        public ObservableCollection<ReportProcedure> Surgeries { get { return _surgeries; } }

        public void generateReport()
        {
            _examinations.Clear();
            _surgeries.Clear();

            foreach (DateTime datetime in getWeek())
            {
                if (DataMockup.Instance.Examinations.ContainsKey(datetime.Date))
                    foreach (Examination examination in DataMockup.Instance.Examinations[datetime.Date])
                        _examinations.Add(new ReportProcedure(examination));

                if (DataMockup.Instance.Surgeries.ContainsKey(datetime.Date))
                    foreach (Surgery surgery in DataMockup.Instance.Surgeries[datetime.Date])
                        _surgeries.Add(new ReportProcedure(surgery));
            }

            Stream xpsFile = GetXPSDocument();
            if (xpsFile != null)
            {
                xpsFile.Position = 0;

                //Initialize XPSToPdfConverter.
                Syncfusion.XPS.XPSToPdfConverter converter = new Syncfusion.XPS.XPSToPdfConverter();

                //Convert XPS document into PDF document.
                PdfDocument document = converter.Convert(xpsFile);

                //Save the Pdf document.
                document.Save("output.pdf");

                //Open the Pdf document
                System.Diagnostics.Process.Start("output.pdf");

                //Close the Pdf document.
                document.Close(true);
            }
            
        }

        private MemoryStream GetXPSDocument()
        {
            //Create visual UIElement.
            var blah = new Report();
            blah.DataContext = this;
            UIElement visual = blah;
            FixedDocument doc = new System.Windows.Documents.FixedDocument();
            PageContent pageContent = new System.Windows.Documents.PageContent();
            FixedPage fixedPage = new System.Windows.Documents.FixedPage();

            //Create first page of document
            fixedPage.Children.Add(visual);
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);

            //Adding page content to pages.
            doc.Pages.Add(pageContent);

            //Create the stream.
            MemoryStream stream = new MemoryStream();

            XpsDocument xpsdocument = new XpsDocument(System.IO.Packaging.Package.Open(stream, FileMode.Create));
            XpsDocumentWriter xpswriter = XpsDocument.CreateXpsDocumentWriter(xpsdocument);

            //Write the XPS document.
            xpswriter.Write(doc);

            //Close the XPS document.
            xpsdocument.Close();

            return stream;
        }
        
        public GenerateReportViewModel() : base("Sedmični izveštaj")
        {
            _surgeries = new ObservableCollection<ReportProcedure>();
            _examinations = new ObservableCollection<ReportProcedure>();

            foreach(DateTime datetime in getWeek())
            {
                if (DataMockup.Instance.Examinations.ContainsKey(datetime.Date))
                    foreach (Examination examination in DataMockup.Instance.Examinations[datetime.Date])
                        _examinations.Add(new ReportProcedure(examination));

                if (DataMockup.Instance.Surgeries.ContainsKey(datetime.Date))
                    foreach (Surgery surgery in DataMockup.Instance.Surgeries[datetime.Date])
                        _surgeries.Add(new ReportProcedure(surgery));
            }
        }

    }
}
