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
using WPFReactiveUI.Interactions;

namespace WPFReactiveUI.ViewModel
{
    public interface IMainViewModel : IRoutableViewModel
    {
        ReactiveCommand<Unit, Unit> NavigateToExcelParser { get; }
        //ReactiveCommand<Unit, Unit> MessageCommand { get; }
    }
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        public string UrlPathSegment
        {
            get { return "MainView"; }
        }
        public IScreen HostScreen { get; protected set; }
        public ReactiveCommand<Unit, Unit> NavigateToExcelParser { get; }

        //public ReactiveCommand<Unit, Unit> MessageCommand { get; protected set; }
        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;

            //MessageCommand = ReactiveCommand.CreateFromObservable(() => MessageInteractions.ShowMessage.Handle("LOL"));
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
    }
}
