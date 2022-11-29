using ReactiveUI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Reactive;
using System;
using System.Collections.Generic;

namespace WPFReactiveUI.ViewModel
{
    public class ExcelViewModel : ReactiveObject, IRoutableViewModel
    {
        private string xlsxFileName;
        private DataTable? dt;
        private DataView? dv;
        private DataTableCollection? tableCollection;

        public IEnumerable<string> Regions { get; } = new List<string>() { "East", "West", "Central" };

        public string XlsxFileName
        {
            get { return xlsxFileName; }
            set { xlsxFileName = value; }
        }

        public ExcelViewModel(IScreen hostscreen)
        {
            HostScreen = hostscreen;

            this.Back = HostScreen.Router.NavigateBack;

            #region <>

            ImportExcel = ReactiveCommand.Create(() =>
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(xlsxFileName.ToString(), 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel.Worksheet excelSheet = (Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                dt = new DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = (string)(excelRange.Cells[1, colCnt] as Excel.Range).Value2;
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (double)(excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }

                dv = dt.AsDataView();

                return dv;

                //excelBook.Close(true, null, null);
                //excelApp.Quit();
            });
            #endregion
        }

        public string UrlPathSegment => "Second";

        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }
        public ReactiveCommand<Unit, DataView> ImportExcel { get; set; }
    }
}
