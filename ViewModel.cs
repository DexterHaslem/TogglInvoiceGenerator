using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting;
using TogglAPI;
using TogglAPI.Models;
using TogglInvoiceGenerator.Annotations;

namespace TogglInvoiceGenerator
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow mainWindow;

        private readonly Api _api;
        
        private Workspace[] _workspaces;
        private Project _selectedProject;
        private Contract _selectedContract;

        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Contract> Contracts { get; private set; }

        public ContactInfo ContactInfo { get; set; }

        public string CompanyName { get; set; }
        public string VendorNo { get; set; }
        public int InvoiceNo { get; set; }
        public string DateStr { get; set; }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                if (Equals(value, _selectedProject)) return;
                _selectedProject = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GenerateReportEnabled));
            }
        }

        public Contract SelectedContract
        {
            get => _selectedContract;
            set
            {
                if (Equals(value, _selectedContract)) return;
                _selectedContract = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DeleteContractEnabled));
                OnPropertyChanged(nameof(AddEditContractButtonText));
            }
        }

        public bool DeleteContractEnabled => SelectedContract != null;

        public string AddEditContractButtonText => SelectedContract != null ? "Edit Contract" : "Add Contract";

        public bool GenerateReportEnabled => mainWindow.projectSelectionListBox.SelectedItems.Count > 0;

        private void CopyPersistenceToUi(Persistence persistence)
        {
            Contracts = new ObservableCollection<Contract>(persistence.Contracts);
            ContactInfo = persistence.ContactInfo;
            CompanyName = persistence.CompanyName;
            VendorNo = persistence.VendorNo;
            InvoiceNo = persistence.InvoiceNo;
            DateStr = persistence.PoP;
        }

        private void CopyUiToPersistence(Persistence persistence)
        {
            persistence.Contracts = Contracts.ToArray();
            persistence.ContactInfo = ContactInfo;
            persistence.CompanyName = CompanyName;
            persistence.VendorNo = VendorNo;
            persistence.InvoiceNo = InvoiceNo;
            persistence.PoP = DateStr;
        }

        public ViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            _api = new Api(new FileInfo("apikey.txt"));

            Projects = new ObservableCollection<Project>();

            var persistence = Persistence.Restore();
            CopyPersistenceToUi(persistence);

            mainWindow.Closed += (sender, args) =>
            {
                var toSave = new Persistence();
                CopyUiToPersistence(toSave);
                toSave.Save();
            }; 
            
            GetTogglApiData();
        }

        private void GetTogglApiData()
        {
            Projects.Clear();
            _workspaces = _api.GetWorkspaces();
            foreach (var workspace in _workspaces)
            {
                var projects = _api.GetProjects(workspace.Id);
                foreach (var project in projects)
                    Projects.Add(project);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void GenerateReport(RoutedEventArgs routedEventArgs)
        {
            var selectedProject = (Project)mainWindow.projectSelectionListBox.SelectedItems[0];
            var detailReport = GetDetailReport(selectedProject);
            /* pdf */
            //const string testFile = "invoice.pdf";
            //PdfExportOptions options = new PdfExportOptions();
            //options.PdfACompatibility = PdfACompatibility.PdfA1b;

            /* docx */
            const string testFile = "invoice.docx";
            var docxExportOptions = new DocxExportOptions();
            docxExportOptions.AllowFloatingPictures = true;
            docxExportOptions.RasterizeImages = false;
            docxExportOptions.RasterizationResolution = 96;
            docxExportOptions.KeepRowHeight = true;

            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }

            var selectedContract = new Contract();
            var report = new Report();
            ((ObjectDataSource)report.DataSource).DataSource = new ReportDataSource(selectedContract, selectedProject, detailReport);
            report.CreateDocument();

            //report.ExportToPdf(testFile, options);
            report.ExportToDocx(testFile, docxExportOptions);
            Process.Start(testFile);
        }

        private DetailReportResponse GetDetailReport(Project selectedProject)
        {
            var detailedReport = _api.GetMonthsDetailedReport(DateTime.Now, _workspaces[0].Id, new []{selectedProject.Id});
            return detailedReport;
        }

        public void OnEditContactInformation(RoutedEventArgs routedEventArgs)
        {
            var editor = new ContactInfoEditorWindow {Owner = mainWindow, ContactInfo = ContactInfo};
            var result = editor.ShowDialog();
            if (result == true)
            {
                ContactInfo = editor.ContactInfo;
            }
        }

        public void OnAddEditContract(RoutedEventArgs routedEventArgs)
        {
            var isEditing = SelectedContract != null;
            // make sure to clone selected here so we can cancel
            var contract = isEditing ? new Contract(SelectedContract) : new Contract();
            var contractEditor = new ContractEditorWindow {Owner = mainWindow, Contract = contract };
            var result = contractEditor.ShowDialog();
            if (result != true) 
                return;

            if (isEditing)
            {
                var oldIdx = Contracts.IndexOf(SelectedContract);
                SelectedContract = null;
                Contracts.RemoveAt(oldIdx);
                Contracts.Insert(oldIdx, contract);
                SelectedContract = contract;
            }
            else
            {
                Contracts.Add(contractEditor.Contract);
            }
        }

        public void OnDuplicateContract(RoutedEventArgs routedEventArgs)
        {
            Contract dupedContract = new Contract(SelectedContract);
            var contractEditor = new ContractEditorWindow { Owner = mainWindow, Contract = dupedContract };
            var result = contractEditor.ShowDialog();
            if (result == true)
            {
                Contracts.Add(contractEditor.Contract);
            }
        }

        public void OnDeleteContract(RoutedEventArgs routedEventArgs)
        {
            var selected = SelectedContract;
            SelectedContract = null;
            Contracts.Remove(selected);
        }
    }
}
