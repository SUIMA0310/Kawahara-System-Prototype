using Microsoft.AspNet.SignalR.Client;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClientApplication.Models;

namespace WindowsClientApplication.Modules {

    [ModuleExport( typeof( MainModule ) )]
    public class MainModule : IModule {

        [Import]
        private IRegionManager RegionManager { get; set; }

        public void Initialize() {

            this.RegionManager.RegisterViewWithRegion( "MainRegion", typeof( Views.MainControl ) );

        }

        [Export(typeof(IRealtimeReactionHub))]
        public IRealtimeReactionHub CreateRealtimeReactionHub => 
            RealtimeReactionHub.CreateRealtimeReactionHub("http://kawahara-system-prototype.azurewebsites.net/");

    }

}
