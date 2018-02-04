using WindowsClientApplication.Views;
using System.Windows;
using Prism.Modularity;
using Prism.Mef;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace WindowsClientApplication {
    class Bootstrapper : MefBootstrapper {

        protected override DependencyObject CreateShell() {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog() {

            this.AggregateCatalog.Catalogs.Add( new AssemblyCatalog( Assembly.GetExecutingAssembly() ) );

        }

    }
}
