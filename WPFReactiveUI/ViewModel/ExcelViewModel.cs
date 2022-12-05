using ReactiveUI;
using System.Data;
using System.Reactive;
using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using WPFReactiveUI.Interactions;
using System.Windows.Input;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Text;
using System.IO;

namespace WPFReactiveUI.ViewModel
{
    public interface IExcelViewModel : IRoutableViewModel
    {
        public ICommand LoadExcelCommand { get; }
        public ICommand FilterDateCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }
        //public ICommand ClearFilterCommand { get; }

        public DateTime DateFrom { get; }

        public DateTime DateTo { get; }
        
        public string Region { get; }
    }
    public class ExcelViewModel : ReactiveObject, IExcelViewModel
    {
        private DataTable? dt;
        private DataView? dv;
        private DataTableCollection? tableCollection;

        public ExcelViewModel(IScreen hostscreen)
        {
            HostScreen = hostscreen;

            this.Back = HostScreen.Router.NavigateBack;

            LoadExcelCommand = ReactiveCommand.CreateFromTask(o =>
            {
                return Task.Run(() =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Excel Files | *.xls;*.xlsx;";
                    if (ofd.ShowDialog() != null)
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                            }
                        }
                    }
                    FileName = ofd.FileName;
                    dt = tableCollection?[0];
                    ExcelFile = dt?.AsDataView();
                });
            });

            FilterDateCommand = ReactiveCommand.Create(FilterByDate);

            //ClearFilterCommand = ReactiveCommand.Create(ClearAllFilter);
            #region <>
            //OpenFileCommand = ReactiveCommand.CreateFromObservable(() => OpenFileInteractions.FileDialog.Handle());


            //ImportExcelCommand = ReactiveCommand.Create(() =>
            //{
            //    OpenFileDialog openFile = new OpenFileDialog();

            //    openFile.Filter = "Excel Files|*.xls;*.xlsx;";
            //    FileName = openFile.FileName;

            //    //if (openFile.ShowDialog() != null) 
            //    //{
            //    //    Excel.Application excelApp = new Excel.Application();
            //    //    Excel.Workbook excelBook = excelApp.Workbooks.Open(FileName.ToString(), 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //    //    Excel.Worksheet excelSheet = (Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
            //    //    Excel.Range excelRange = excelSheet.UsedRange;

            //    //    string strCellData = "";
            //    //    double douCellData;
            //    //    int rowCnt = 0;
            //    //    int colCnt = 0;

            //    //    dt = new DataTable();
            //    //    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            //    //    {
            //    //        string strColumn = "";
            //    //        strColumn = (string)(excelRange.Cells[1, colCnt] as Excel.Range).Value2;
            //    //        dt.Columns.Add(strColumn, typeof(string));
            //    //    }

            //    //    for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            //    //    {
            //    //        string strData = "";
            //    //        for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            //    //        {
            //    //            try
            //    //            {
            //    //                strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
            //    //                strData += strCellData + "|";
            //    //            }
            //    //            catch (Exception ex)
            //    //            {
            //    //                douCellData = (double)(excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
            //    //                strData += douCellData.ToString() + "|";
            //    //            }
            //    //        }
            //    //        strData = strData.Remove(strData.Length - 1, 1);
            //    //        dt.Rows.Add(strData.Split('|'));
            //    //    }
            //    //}

            //    //dv = dt.AsDataView();
            //    //ExcelFile = dv;
            //    //try
            //    //{

            //    //}
            //    //catch(Exception ex)
            //    //{
            //    //    MessageBox.Show(ex.Message);
            //    //}
            //    //return dv ?;
            //    //excelBook.Close(true, null, null);
            //    //excelApp.Quit();
            //});
            #endregion
        }

        public string UrlPathSegment => "ExcelViewModel View";

        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }

        #region <Properties>
        private DataView _excelFile;
        private string _fileName;
        private string _searchKeyword;
        private string _region;
        private DateTime _dateFrom = DateTime.Now;
        private DateTime _dateTo = DateTime.Now;
        private List<string> _regions = new List<string>() { "East", "West", "Central" };

        public IEnumerable<string> Regions
        {
            get { return _regions; }
        }

        public DataView ExcelFile
        {
            get => this._excelFile;
            set => this.RaiseAndSetIfChanged(ref this._excelFile, value);
        }

        public string FileName
        {
            get => this._fileName;
            set => this.RaiseAndSetIfChanged(ref this._fileName, value);
        }

        public string SearchKeyword
        {
            get => this._searchKeyword;
            set
            {
                this.RaiseAndSetIfChanged(ref this._searchKeyword, value);
                {
                    DataView dv = _excelFile;
                    if (dv != null)
                    {
                        dv.RowFilter = (SearchKeyword != string.Empty) ? dv.RowFilter = $"{"Rep"} LIKE '%{SearchKeyword}%'" : null;
                    }
                }
            }
        }
        public string Region
        {
            get => this._region;
            set
            {
                this.RaiseAndSetIfChanged(ref this._region, value);
                {
                    DataView dv = _excelFile;

                    if (dv != null)
                    {
                        dv.RowFilter = (Region != null) ? dv.RowFilter = $"{"Region"}='{Region}'" : null;
                    }
                }
            }
        }



        public DateTime DateFrom
        {
            get => this._dateFrom;
            set => this.RaiseAndSetIfChanged(ref this._dateFrom, value);
        }

        public DateTime DateTo
        {
            get => this._dateTo;
            set => this.RaiseAndSetIfChanged(ref this._dateTo, value);
        }
        #endregion

        #region <Commands>

        public ICommand LoadExcelCommand { get; }
        public ICommand FilterDateCommand { get; }
        //public ICommand ClearFilterCommand { get; }
        #endregion

        #region <Methods>

        private void ClearAllFilter()
        {
            try
            {
                if (dv?.RowFilter != null)
                    dv = ExcelFile;
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void FilterByDate()
        {
            DataView dv = _excelFile;
            if (dv != null)
            {
                dv.RowFilter = $"OrderDate >= '{DateFrom}' AND OrderDate <= '{DateTo}'";
            }
        }
        #endregion
    }
}
