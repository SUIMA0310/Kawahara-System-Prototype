using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using CommonTypes;

namespace WebApplicationServer.Hubs {

    /// <summary>
    /// SignalRのハブクラスです
    /// Client(ブラウザ含む)はここに接続します
    /// </summary>
    public class RealtimeReactionHub : Hub {

        private readonly string Listener = "Listener";

        /// <summary>
        /// Clientから呼び出し可能なメソッド
        /// 
        /// Reaction.Good をListenerに伝達します。
        /// </summary>
        public void Good() {

            Clients.Group( Listener ).Reaction( ReactionTypes.Good );

        }

        /// <summary>
        /// Clientから呼び出し可能なメソッド
        /// 
        /// 自身がListenerであることをハブに通知します。
        /// </summary>
        public Task AddListener() {

            return Groups.Add( Context.ConnectionId, Listener );

        }

        /// <summary>
        /// Clientから呼び出し可能なメソッド
        /// 
        /// 自身がListenerを辞めることをハブに通知します。
        /// </summary>
        public Task RemoveListener() {

            return Groups.Remove( Context.ConnectionId, Listener );

        }

        /// <summary>
        /// Client切断時に呼び出されます。
        /// </summary>
        /// <param name="stopCalled">明示的に切断されたか</param>
        public override async Task OnDisconnected(bool stopCalled) {
            //Listenerから抜けます。
            await this.RemoveListener();
            await base.OnDisconnected( stopCalled );
        }

    }

}