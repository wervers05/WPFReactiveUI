using ReactiveUI;
using System.Windows;
using WPFReactiveUI.ViewModel;

namespace WPFReactiveUI.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : IViewFor<IMainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
                d(this.BindCommand(ViewModel, vm => vm.NavigateToExcelParser, view => view.navigateButton));
                //d(this.BindCommand(ViewModel, vm => vm.MessageCommand, view => view.messageButton));
            });
        }

        public IMainViewModel ViewModel
        {
            get { return (IMainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IMainViewModel), typeof(MainView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainViewModel)value; }
        }
    }
}
