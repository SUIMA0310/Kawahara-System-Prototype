using DryIoc;
using Microsoft.AspNet.SignalR.Client;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClientApplication.Models;

namespace WindowsClientApplication.Modules {

    public class MainModule : IModule {

        private IContainer Container { get; }
        private IRegionManager RegionManager { get; }

        public MainModule(IContainer container, IRegionManager region) {

            this.Container = container;
            this.RegionManager = region;

        }

        public void Initialize() {

            this.Container.Register( 
                made: Made.Of( () => RealtimeReactionHub.CreateRealtimeReactionHub( "http://localhost:52645/" ) )
            );
            this.Container.Register<Views.MainControl>();
            this.RegionManager.RegisterViewWithRegion( "MainRegion", typeof( Views.MainControl ) );

        }

    }

}
