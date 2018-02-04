using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using WindowsClientApplication.Models;

namespace WindowsClientApplication.ViewModels {

    [Export]
    public class MainControlViewModel : BindableBase, IReactionNotification {

        
        private IRealtimeReactionHub RealtimeReactionHub { get; }
        

        public event EventHandler<ReactionEventArgs> ReactionReceived;

        [ImportingConstructor]
        public MainControlViewModel(IRealtimeReactionHub realtimeReactionHub) {

            this.RealtimeReactionHub = realtimeReactionHub;

            this.RealtimeReactionHub.OnReaction( x => {

                this.OnReactionReceived( new ReactionEventArgs( x ) );

            } );
            this.RealtimeReactionHub.AddListener();

        }

        protected void OnReactionReceived(ReactionEventArgs args) {

            ReactionReceived?.Invoke( this, args );

        }
    }

}
