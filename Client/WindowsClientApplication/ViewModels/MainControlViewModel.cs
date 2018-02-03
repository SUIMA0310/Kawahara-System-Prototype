using Prism.Mvvm;
using System.Collections.Generic;
using WindowsClientApplication.Models;

namespace WindowsClientApplication.ViewModels {
    public class MainControlViewModel : BindableBase {

        
        private Container<ReactionInfo> Container;
        private IRealtimeReactionHub RealtimeReactionHub { get; }


        public IEnumerable<ReactionInfo> Reactions => Container; 


        public MainControlViewModel(IRealtimeReactionHub realtimeReactionHub) {

            this.Container = new Container<ReactionInfo>();
            this.RealtimeReactionHub = realtimeReactionHub;

            this.RealtimeReactionHub.OnReaction( x => {

                this.Container.Insert( new ReactionInfo( x ) );

            } );
            this.RealtimeReactionHub.AddListener();

        }
    }
}
