using System;
using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    /// <summary>        
    /// For mapping modules        
    /// </summary>        
    public class ModuleLocators : IModule
    {
        #region private properties        
        /// <summary>        
        /// Instance of IRegionManager        
        /// </summary>        
        private IRegionManager _regionManager;

        #endregion

        #region Constructor        
        /// <summary>        
        /// parameterized constructor initializes IRegionManager        
        /// </summary>        
        /// <param name="regionManager"></param>        
        public ModuleLocators(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        #endregion

        #region Interface methods     
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("Shell", typeof(View.WithdrawalView));    
        }
      
        /// <param name="containerRegistry"></param>        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
        #endregion
    }
}
