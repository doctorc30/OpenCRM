using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.ViewModels.Events;
using bbom.Data;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class EventsHelper : IEventsHelper
    {
        private readonly IMapper _mapper;

        public EventsHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public EventJson GetEventJson(int eventId)
        {
            var evnt = DataFasade.GetRepository<Event>().GetAll().SingleOrDefault(e => e.Id.Equals(eventId));
            return _mapper.Map<EventJson>(evnt);
        }

        public ICollection<EventJson> GetEventsJson()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<EventJson> GetEventsJson(ICollection<Event> events)
        {
            return _mapper.Map<List<EventJson>>(events);
            //return (from evnt in events
            //    let spiker = evnt.Spikers.FirstOrDefault()
            //    select new EventJson
            //    {
            //        name = evnt.Id.ToString(),
            //        title = evnt.Title,
            //        url = evnt.Url,
            //        start = evnt.StartDate.ToString("yyyy-MM-dd HH:mm"),
            //        end = evnt.EndDate.ToString("yyyy-MM-dd HH:mm"),
            //        typeId = evnt.TypeId.ToString(),
            //        userName = evnt.AspNetUser.GetFio(),
            //        isShowDate = evnt.StartDate < DateTime.Now.AddHours(24),
            //        spiker = spiker == null ? evnt.Spiker : spiker.GetIO(),
            //        icon =
            //            spiker == null
            //                ? GlobalConstants.ImageEventsPath + evnt.Icon
            //                : GlobalConstants.ImageUserProfilePath + spiker.Id,
            //        description = evnt.Description
            //    }).ToList();
        }
    }
}