using Prism.Unity;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithdrawalMachine
{
    /// <summary>
    /// We are using this class to load the Shell
    /// </summary>
    [Obsolete]
    public class BootStrapper : UnityBootstrapper
    {    
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
        }
   
        /// Initializes shell.xaml           
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }
     
        /// loads the Shell.xaml        
        protected override void InitializeShell()
        {
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
   
        /// Add view(module) from other assemblies and begins with modularity         
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            Type ModuleLocatorType = typeof(Presentation.ModuleLocators);
            ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo
            {
                ModuleName = ModuleLocatorType.Name,
                ModuleType = ModuleLocatorType.AssemblyQualifiedName
            });
        }
    }
}
