using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace bbom.Admin.Core.SignalR.Hubs
{
    [HubName("event")]
    public class EventHub : Hub
    {
        public static void  RefreshNearestEvent()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            hubContext.Clients.AllExcept().refreshEvent();
        }

        public void UpdateCalendar()
        {
            Clients.Others.updateCalendarSR();
        }
    }
}