using WindowsClientApplication.Views;
using System.Windows;
using Prism.Modularity;
using DryIoc;
using Prism.DryIoc;

namespace WindowsClientApplication {
    class Bootstrapper : DryIocBootstrapper {

        protected override DependencyObject CreateShell() {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog() {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule( typeof( Modules.MainModule ) );
        }

    }
}
