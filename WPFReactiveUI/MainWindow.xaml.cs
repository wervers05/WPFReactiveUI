using Splat;
using System.Windows;
using WPFReactiveUI.ViewModel;
using System.Reflection;
using ReactiveUI;
using System.Windows.Controls;


namespace WPFReactiveUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppBootstrapper AppBootstrapper { get; protected set; }

        public MainWindow()
        {
            InitializeComponent();

            AppBootstrapper = new AppBootstrapper();
            DataContext = AppBootstrapper;
        }
    }
}
