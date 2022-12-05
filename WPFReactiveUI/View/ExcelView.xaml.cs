using ReactiveUI;
using System.Windows;
using WPFReactiveUI.ViewModel;

namespace WPFReactiveUI.View
{
    /// <summary>
    /// Interaction logic for ExcelView.xaml
    /// </summary>
    public partial class ExcelView : IViewFor<IExcelViewModel>
    {
        public ExcelView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
                // Property Bindings
                d(this.Bind(ViewModel, vm => vm.DateFrom, view => view.dtpFrom.SelectedDate));
                d(this.Bind(ViewModel, vm => vm.DateTo, view => view.dtpTo.SelectedDate));
                //d(this.Bind(ViewModel, vm => vm.Region, view => view.cmbFilterRegion.SelectedValue));
                //d(this.OneWayBind(ViewModel, vm => vm.FileName, view => view.fileNameTextBox.Text));
                //d(this.WhenAnyValue(vm => vm.ViewModel.ExcelFile).BindTo(this, view => view.excelDataGrid.ItemsSource));
                //d(this.WhenAnyValue(vm => vm.ViewModel.Regions).BindTo(this, view => view.cmbFilterRegion.ItemsSource));

                //d(this.OneWayBind(ViewModel, vm => vm.Regions, view => view.cmbFilterRegion.ItemsSource));
                //d(this.OneWayBind(ViewModel, vm => vm.ExcelFile, view => view.excelDataGrid.ItemsSource));


                // Command Bindings
                d(this.BindCommand(ViewModel, vm => vm.Back, view => view.backButton));
                d(this.BindCommand(ViewModel, vm => vm.LoadExcelCommand, view => view.btnImportFile));
                d(this.BindCommand(ViewModel, vm => vm.FilterDateCommand, view => view.btnDateFilter));

                



                //d(this.BindCommand(ViewModel, vm => vm.ImportExcelCommand, view => view.btnOpenFileDialog));
            });

        }
        public IExcelViewModel ViewModel
        {
            get { return (IExcelViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IExcelViewModel), typeof(ExcelView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ExcelViewModel)value; }
        }
    }
}
