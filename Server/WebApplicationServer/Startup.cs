using Microsoft.Owin;
using Owin;

[assembly: OwinStartup( typeof( WebApplicationServer.Startup ) )]

namespace WebApplicationServer {

    public class Startup {

        public void Configuration(IAppBuilder app) {

            app.MapSignalR();

        }

    }

}
