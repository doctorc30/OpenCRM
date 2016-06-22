using System.Collections.Generic;
using bbom.Admin.Core.ViewModels.Events;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface IEventsHelper
    {
        EventJson GetEventJson(int eventId);
        ICollection<EventJson> GetEventsJson();
        ICollection<EventJson> GetEventsJson(ICollection<Event> events);
    }
}