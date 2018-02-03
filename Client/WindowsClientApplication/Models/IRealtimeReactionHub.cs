using System;
using System.Threading.Tasks;
using CommonTypes;

namespace WindowsClientApplication.Models {

    public interface IRealtimeReactionHub {
        Task Good();
        Task AddListener();
        Task RemoveListener();
        IDisposable OnReaction(Action<ReactionTypes> onData);
    }

}