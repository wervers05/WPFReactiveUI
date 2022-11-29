using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using WPFReactiveUI.View;

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
    }
}
