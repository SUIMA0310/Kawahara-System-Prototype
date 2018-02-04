using System;
using CommonTypes;

namespace WindowsClientApplication.Models {
    public class ReactionEventArgs : EventArgs {

        public ReactionTypes ReactionTypes { get; }

        public ReactionEventArgs(ReactionTypes reactionTypes) {
            this.ReactionTypes = reactionTypes;
        }

    }

}
