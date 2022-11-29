using ReactiveUI;

namespace WPFReactiveUI.View
{
    /// <summary>
    /// Interaction logic for ExcelView.xaml
    /// </summary>
    public partial class ExcelView
    {
        public ExcelView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.BindCommand(ViewModel, vm => vm.Back, view => view.backButton));
                d(this.BindCommand(ViewModel, vm => vm.ImportExcel, view => view.btnOpenFileDialog));
            });

        }
    }
}
