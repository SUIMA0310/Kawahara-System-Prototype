using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClientApplication.Util;
using CommonTypes;

namespace WindowsClientApplication.Models {

    public class RealtimeReactionHub : IRealtimeReactionHub {

        public static IRealtimeReactionHub CreateRealtimeReactionHub(string url) {

            // Hubのあるサイトへの接続
            var conn = new HubConnection( url );
            // HubConnectionをStartする前にRealtimeReactionHubクラスに対するproxyを作っておく
            var proxy = conn.CreateHubProxy( "realtimeReactionHub" );

            var hub = new RealtimeReactionHub( proxy );

            // 接続開始
            conn.Start().Wait();

            return hub;

        }

        private readonly IHubProxy proxy;


        public RealtimeReactionHub(IHubProxy proxy) {

            this.proxy = proxy;

        }

        public IDisposable OnReaction(Action<ReactionTypes> onData) {

            return this.proxy.On( onData );

        }

        public Task Good() {

            return this.proxy.Invoke();

        }

        public Task AddListener() {

            return this.proxy.Invoke();

        }

        public Task RemoveListener() {

            return this.proxy.Invoke();

        }
    }

}
