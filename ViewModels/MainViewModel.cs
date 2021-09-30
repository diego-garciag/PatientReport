using DVHPlot.ViewModels;
using Microsoft.Win32;
using OxyPlot.Wpf;
using PatientReport.Views;
using PDFtoAria;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using VMS.TPS.Common.Model.API;

namespace PatientReport.ViewModels
{
    public class MainViewModel
    {
   
        public MainViewModel(PatientViewModel patientViewModel, 
            PlanViewModel planViewModel, 
            FieldViewModel fieldViewModel,
            DVHViewModel dVHViewModel,
            DVHSelectionViewModel dVHSelectionViewModel,
            Patient patient,
            User user
            )
        {
            _patient = patient;
            _user = user;
            PatientViewModel = patientViewModel;
            PlanViewModel = planViewModel;
            FieldViewModel = fieldViewModel;
            DVHViewModel = dVHViewModel;
            DVHSelectionViewModel = dVHSelectionViewModel;

            PrintCommand = new DelegateCommand(OnPrint);
            AriaPostCommand = new DelegateCommand(OnPostToAria);
        }

        private void OnPostToAria()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "PDF (*.pdf)|*.pdf";
            if (file.ShowDialog() == true)
            {
                var BinaryContent = File.ReadAllBytes(file.FileName);
                // change "Consent" to the correct type
                CustomInsertDocumentsParameter.PostDocumentData(_patient.Id, _user,
                    BinaryContent, "Plan Report",
                    new VMS.OIS.ARIALocal.WebServices.Document.Contracts.DocumentType { DocumentTypeDescription = "Consent" });
            }
        }

        private void OnPrint()
        {
            FlowDocument fd = new FlowDocument { FontSize = 12, FontFamily = new System.Windows.Media.FontFamily("Franklin Gothic") };
            fd.Blocks.Add(new Paragraph(new Run("Treatment Plan Report")));
            fd.Blocks.Add(new BlockUIContainer(new PatientView { DataContext = PatientViewModel.PatientInfo }));
            fd.Blocks.Add(new BlockUIContainer(new PlanView { DataContext = PlanViewModel.PlanInfo }));
            foreach (var field in FieldViewModel.Fields)
            {
                fd.Blocks.Add(new BlockUIContainer(new FieldDetailsView { DataContext = field }));
            }
            BitmapSource bmp = new PngExporter().ExportToBitmap(DVHViewModel.DVHPlotModel);
            fd.Blocks.Add(new BlockUIContainer(new System.Windows.Controls.Image
            {
                Source = bmp,
                Height = 700,
                Width = 900
            }));
            System.Windows.Controls.PrintDialog printer = new System.Windows.Controls.PrintDialog();
            printer.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
            fd.PageHeight = 816;
            fd.PageWidth = 1056;
            fd.PagePadding = new System.Windows.Thickness(50);
            fd.ColumnGap = 0;
            fd.ColumnWidth = 1056;
            IDocumentPaginatorSource source = fd;
            if (printer.ShowDialog() == true) // microsoft print2pdf
            {
                printer.PrintDocument(source.DocumentPaginator, "TreatmentPlanReport");
            }
        }

        public PatientViewModel PatientViewModel { get; }
        public PlanViewModel PlanViewModel { get; }
        public FieldViewModel FieldViewModel { get; }
        public DVHSelectionViewModel DVHSelectionViewModel { get; }
        public DelegateCommand PrintCommand { get; private set; }
        public DelegateCommand AriaPostCommand { get; private set; }
        public DVHViewModel DVHViewModel { get; }
        public Patient _patient { get; private set; }
        public User _user { get; private set; }
    }
}
