using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClientApplication.Models {

    public interface IReactionNotification {

        event EventHandler<ReactionEventArgs> ReactionReceived;

    }

}
