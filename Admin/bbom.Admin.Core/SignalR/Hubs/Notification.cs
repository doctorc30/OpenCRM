using bbom.Admin.Core.Notifications;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace bbom.Admin.Core.SignalR.Hubs
{
    [HubName("Notification")]
    public class Notification : Hub
    {
        public static void Notify(Alert alert)
        {
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            //hubContext.Clients.AllExcept().notifyClient(alert);
        }
    }
}