using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFReactiveUI.ViewModel
{
    public interface IMainViewModel : IRoutableViewModel
    {
        ReactiveCommand<Unit, Unit> NavigateToExcelParser { get; }
    }
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        public string UrlPathSegment
        {
            get { return "welcome"; }
        }
        public IScreen HostScreen { get; protected set; }
        public ReactiveCommand<Unit, Unit> NavigateToExcelParser { get; }
        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;

            //HelloWorld = ReactiveCommand.CreateFromObservable(() => MessageInteractions.ShowMessage.Handle("It works!!!"));
            NavigateToExcelParser = ReactiveCommand.CreateFromTask(async () => await HostScreen.Router.Navigate.Execute(new ExcelViewModel(HostScreen)).Select(_ => Unit.Default));

            this.WhenNavigatedTo(() => Bar());
        }
        private IDisposable Bar()
        {
            return Disposable.Create(() => Foo());
        }

        private void Foo()
        {
            if (true) { }
        }
        #region <-----Query----->
        //private int _totalRows;
        //private string _searchTerm;
        //private string _region;
        //private string _excelSheet = "SalesOrders";
        //private string _filePath;
        //private DataView _excelFile = new();
        //private DateTime _startDate;
        //private DateTime _endDate;

        //public string Region
        //{
        //    get => this._region;
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref this._region, value);
        //        {
        //            DataView dv = _excelFile;

        //            try
        //            {
        //                if (dv != null)
        //                {
        //                    if (Region != null)
        //                    {
        //                        dv.RowFilter = String.Format("{0}='{1}'", "Region", Region);
        //                        TotalRows = ExcelFile.Count;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //    }
        //}

        //public DataView ExcelFile
        //{
        //    get => this._excelFile;
        //    set => this.RaiseAndSetIfChanged(ref this._excelFile, value);
        //}
        //public DateTime StartDate
        //{
        //    get => this._startDate;
        //    set => this.RaiseAndSetIfChanged(ref this._startDate, value);
        //}

        //public DateTime EndDate
        //{
        //    get => this._endDate;
        //    set => this.RaiseAndSetIfChanged(ref _endDate, value);
        //}

        //public int TotalRows
        //{
        //    get => this._totalRows;
        //    set => this.RaiseAndSetIfChanged(ref this._totalRows, value);
        //}

        //public string SearchTerm
        //{
        //    get => _searchTerm;
        //    set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        //}
        #endregion
    }
}
