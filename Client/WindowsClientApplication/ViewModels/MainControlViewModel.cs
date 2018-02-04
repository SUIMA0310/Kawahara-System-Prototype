using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using WindowsClientApplication.Models;

namespace WindowsClientApplication.ViewModels {

    [Export]
    public class MainControlViewModel : BindableBase {

        
        private Container<ReactionInfo> Container;
        private IRealtimeReactionHub RealtimeReactionHub { get; }


        public IEnumerable<ReactionInfo> Reactions => Container; 

        [ImportingConstructor]
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
