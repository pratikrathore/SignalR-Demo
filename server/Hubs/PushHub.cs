using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace server.Hubs
{
    /// <summary>
    /// Defines the events associated with the Hub.
    /// </summary>
    public interface IPushClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workstation"></param>
        /// <returns></returns>
        Task Push(string workstation);
    }

    /// <summary>
    /// Used for providing realtime updates for the PushController.
    /// </summary>
    public class PushHub : Hub<IPushClient>
    {
        public async Task Push(string workstation) => await Clients.All.Push(workstation);
    }
}