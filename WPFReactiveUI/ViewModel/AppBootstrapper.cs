using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using WPFReactiveUI.View;
using System.Data;
using System.Windows;

namespace WPFReactiveUI.ViewModel
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }

        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState testRouter = null)
        {
            Router = testRouter ?? new RoutingState();
            dependencyResolver = dependencyResolver ?? Locator.CurrentMutable;

            // Bind 
            RegisterParts(dependencyResolver);
            // TODO: This is a good place to set up any other app startup tasks

            // Navigate to the opening page of the application
            Router.Navigate.Execute(new MainViewModel(this));
        }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));
            dependencyResolver.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));
            dependencyResolver.Register(() => new ExcelView(), typeof(IViewFor<ExcelViewModel>));
        }

        #region Props
        //private DataView _excelFile;
        //private string _fileName;
        //private string _searchKeyword;
        //private string _region;
        //private DateTime _dateFrom;
        //private DateTime _dateTo;
        //private ExcelViewModel _ExcelVM;

        //public ExcelViewModel ExcelVM
        //{
        //    get { return _ExcelVM; }
        //    set { this.RaiseAndSetIfChanged(ref _ExcelVM, value); }
        //}
        
        //public DataView ExcelFile
        //{
        //    get => this._excelFile;
        //    set => this.RaiseAndSetIfChanged(ref this._excelFile, value);
        //}

        //public string FileName
        //{
        //    get => this._fileName;
        //    set => this.RaiseAndSetIfChanged(ref this._fileName, value);
        //}

        //public string SearchKeyword
        //{
        //    get => this._searchKeyword;
        //    set => this.RaiseAndSetIfChanged(ref this._searchKeyword, value);
        //}

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

        //public DateTime DateFrom
        //{
        //    get => this._dateFrom;
        //    set => this.RaiseAndSetIfChanged(ref this._dateFrom, value);
        //}

        //public DateTime DateTo
        //{
        //    get => this._dateTo;
        //    set => this.RaiseAndSetIfChanged(ref this._dateTo, value);
        //}
        #endregion
    }
}
