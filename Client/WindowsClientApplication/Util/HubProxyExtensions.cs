using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClientApplication.Util {

    public static class HubProxyExtensions {

        public static IDisposable On<T>(this IHubProxy self, Action<T> onData, [CallerMemberName] string eventName = null) {

            return self.On<T>( eventName.Substring( "On".Length ), onData );

        }

        public static Task Invoke(this IHubProxy self, [CallerMemberName] string method = null, params object[] args) {

            return self.Invoke( method, args );

        }

    }

}
