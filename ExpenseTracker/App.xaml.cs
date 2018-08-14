using ExpenseTracker.ViewModels.ViewModelFactories;
using ExpenseTracker.Views;
using Ninject;
using System;
using System.Windows;

namespace ExpenseTracker
{
    internal partial class App : Application
    {
        private readonly Lazy<IKernel> container;

        public App()
        {
            container = new Lazy<IKernel>(CreateContainer);
        }

        private IKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind<IViewModelFactory>().To<ViewModelFactory>().InSingletonScope();

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var view = container.Value.Get<MainWindowView>();
            view.Show();
        }
    }
}