using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace WindowsClientApplication.Models {

    public struct ReactionInfo {

        public TimeSpan TimeSpan => DateTime.Now - Time;
        public DateTime Time { get; }
        public ReactionTypes Reaction { get; }

        public ReactionInfo( ReactionTypes reactionTypes ) {

            this.Time = DateTime.Now;
            this.Reaction = reactionTypes;

        }

    }

}
