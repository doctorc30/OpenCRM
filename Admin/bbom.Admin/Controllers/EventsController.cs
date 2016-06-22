using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using bbom.Admin.Core;
using bbom.Admin.Core.Exceptions;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.SignalR.Hubs;
using bbom.Admin.Core.ViewModels.Events;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.IdentityModelPartials.Comparers;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace bbom.Admin.Controllers
{
    [Authorize]
    //[HandleJsonError]
    public class EventsController : Controller
    {
        private readonly IRepository<Event> _eventsRepository;
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public EventsController(IRepository<Event> eventsRepository, IRepository<AspNetUser> usersRepository,
            IAccessService accessService, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _usersRepository = usersRepository;
            _accessService = accessService;
            _mapper = mapper;
        }

        public ActionResult Index(int id)
        {
            var evnt = _eventsRepository.GetById(id);
            if (DateTime.Now < evnt.StartDate.AddMinutes(-10))
            {
                return View("Info", (object) "Событие еще не началось");
            }
            var user = _usersRepository.GetById(User.GetUserId());
            if (user.AspNetRoles.Any(role => role.AccessToEventType.Contains(evnt.EventType)))
            {
                var inRoom = VebinarRoom.Visitors.Any(uEv => uEv.EventId == id && uEv.Name == user.UserName);
                if (evnt.Stats != null && evnt.Stats == EventStatus.Run && evnt.StartDate.AddMinutes(5) < DateTime.Now &&
                    !inRoom)
                {
                    return View("Info", (object) "Событие уже идет");
                }
                //todo выводить ошибку о том что нету значения настройки
                if (evnt.Url != null &&
                    CoreFasade.SettingsHelper.GetSetting(SettingType.EventLinks)
                        .Values.Any(value => evnt.Url.Contains(value)))
                {
                    return RedirectPermanent(evnt.Url);
                }
                return View(evnt);
            }
            throw new NoPermisionException();
        }

        [HttpPost]
        public ActionResult CheckAccess(int id)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var evnt = _eventsRepository.GetById(id);
            if (user.AspNetRoles.Any(role => role.AccessToEventType.Contains(evnt.EventType)))
            {
                var inRoom = VebinarRoom.Visitors.Any(uEv => uEv.EventId == id && uEv.Name == user.UserName);
                if (evnt.Stats != null && evnt.Stats == EventStatus.Run && evnt.StartDate.AddMinutes(5) < DateTime.Now &&
                    !inRoom)
                {
                    Notification.Notify(Alert.ShowError("Событие уже идет"));
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OnlyRole(Role = UserRole.Spiker)]
        public async Task<JsonResult> Start(EventJson evnt)
        {
            var ev = _eventsRepository.GetById(Convert.ToInt32(evnt.name));
            ev.Stats = EventStatus.Run;
            await _eventsRepository.SaveChangesAsync();
            return Json(Alert.Success);
        }

        [HttpPost]
        [OnlyRole(Role = UserRole.Spiker)]
        public async Task<JsonResult> Stop(EventJson evnt)
        {
            var ev = _eventsRepository.GetById(Convert.ToInt32(evnt.name));
            ev.Stats = EventStatus.End;
            ev.Url = "";
            VebinarRoom.Visitors.RemoveAll(user => user.EventId == ev.Id);
            await _eventsRepository.SaveChangesAsync();
            return Json(Alert.Success);
        }

        [HttpPost]
        [OnlyRole(Role = UserRole.Spiker)]
        public async Task<JsonResult> UpRoles(EventJson evnt)
        {
            try
            {
                var ev = _eventsRepository.GetById(Convert.ToInt32(evnt.name));
                var users = VebinarRoom.Users.Where(user => user.EventId == ev.Id);
                var usersBd = _usersRepository.GetAll();
                var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                foreach (var user in users)
                {
                    var userBd = usersBd.Single(netUser => netUser.UserName == user.Name);
                    await CoreFasade.UsersHelper.ActionUserRoleAsync(userBd.UserName, userBd.Id, UserRole.NotWatch, HttpContext,
                        (id, role) => userManager.RemoveFromRoleAsync(id, role));
                }
                VebinarRoom.UpdateStartBisnesButton(ev.Id);
                return Json(Alert.Success);
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        // POST: Add
        [HttpPost]
        [OnlyAdminAuthorize]
        public async Task<JsonResult> Add(EventJson evnt)
        {
            string start;
            string end;
            try
            {
                start = DateTime.Parse(evnt.start).ToString("yyyy-MM-dd HH:mm");

            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message + "\n start = " + evnt.start), JsonRequestBehavior.AllowGet);
            }
            try
            {
                end = DateTime.Parse(evnt.end).ToString("yyyy-MM-dd HH:mm");

            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message + "\n end = " + evnt.end), JsonRequestBehavior.AllowGet);
            }
            //todo мапить 
            var newEvent = new Event
            {
                Title = evnt.title,
                Url = evnt.url,
                EndDate = Convert.ToDateTime(end),
                StartDate = Convert.ToDateTime(start),
                TypeId = Convert.ToInt32(evnt.typeId),
                UserId = User.GetUserId(),
                Spiker = evnt.spiker,
                Icon = evnt.icon,
                Description = evnt.description
            };
            if (evnt.spikerId != null)
            {
                var userSpiker = _usersRepository.GetById(evnt.spikerId);
                if (userSpiker!=null)
                {
                    newEvent.EventSpikers.Add(new EventSpiker
                    {
                        Event = newEvent,
                        SpikerId = evnt.spikerId,
                    });
                    newEvent.Spiker = userSpiker.GetIO();
                    newEvent.Icon = GlobalConstants.ImageUserProfilePath + userSpiker.Id;
                }
            }
            await _eventsRepository.InsertAsync(newEvent);
            EventHub.RefreshNearestEvent();
            return Json(Alert.ShowSuccess("добавлено"), JsonRequestBehavior.AllowGet);
        }

    

        // POST: Edit
        [HttpPost]
        [OnlyAdminAuthorize]
        public async Task<JsonResult> Edit(EventJson evnt)
        {
            int id = Convert.ToInt32(evnt.name);
            var oldEvent = _eventsRepository.GetAll().Single(oldevent => oldevent.Id == id);
            oldEvent.Url = evnt.url;
            oldEvent.EndDate = Convert.ToDateTime(evnt.end);
            oldEvent.StartDate = Convert.ToDateTime(evnt.start);
            oldEvent.Title = evnt.title;
            oldEvent.Spiker = evnt.spiker;
            oldEvent.Icon = evnt.icon;
            oldEvent.Description = evnt.description;
            if (evnt.typeId != null)
                oldEvent.TypeId = Convert.ToInt32(evnt.typeId);
            if (evnt.spikerId != null)
            {
                oldEvent.EventSpikers.Clear();
                oldEvent.EventSpikers.Add(new EventSpiker
                {
                    Event = oldEvent,
                    SpikerId = evnt.spikerId,
                });
            }
            await _eventsRepository.EditAsync(oldEvent);
            EventHub.RefreshNearestEvent();
            return Json(Alert.Success, JsonRequestBehavior.AllowGet);
        }

        // GET: GetAllJson
        public ActionResult GetAllJson()
        {
            var events = _eventsRepository.GetAll();
            if (events.Any())
            {
                List<EventLigthJson> eventsJson = _mapper.Map<List<EventLigthJson>>(events);
                var data = eventsJson.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(new {}, JsonRequestBehavior.AllowGet);
        }

        // GET: GetLastJson
        public ActionResult GetLastJson()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var alowEvents = _accessService.GetUserAlowEvents(user);
            var events = alowEvents.Where(e => e.StartDate >= DateTime.Now).ToList();
            if (events.Any())
            {
                events.Sort(new EventComparer());
                var lastEvent = events.First();
                if (lastEvent.StartDate >= DateTime.Now && lastEvent.StartDate <= DateTime.Now.AddHours(24))
                    return Json(_mapper.Map<EventJson>(lastEvent), JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GET: GetEventJson Получает информацию о событии
        public ActionResult GetEventJson(int id)
        {
            var events = _eventsRepository.GetById(id);
            return Json(_mapper.Map<EventJson>(events), JsonRequestBehavior.AllowGet);
        }

        // GET: GetArhiveJson
        public JsonResult GetArhiveJson()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var alowEvents = _accessService.GetUserAlowEvents(user);
            //todo вынести в настройки
            var events = alowEvents.Where(e => e.EndDate < DateTime.Now && e.EventType.Id != EventTypeConst.Trening);
            var eventsList = events.ToList();
            eventsList.Sort(new EventComparer());
            if (eventsList.Any())
            {
                List<EventListJson> eventsJson = _mapper.Map<List<EventListJson>>(eventsList);
                eventsJson.Sort(new EventJsonComparer());
                eventsJson.Reverse();
                var data = eventsJson.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = 400;
            return Json(Alert.ShowInfo("Архив пуст"), JsonRequestBehavior.AllowGet);
        }

        // GET: GetLastJson
        public JsonResult GetEventsJson(int? typeId)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var events =
                _accessService.GetUserAlowEvents(user).ToList().Where(e => e.StartDate.AddMinutes(5) > DateTime.Now);
            events = typeId == null ? events : events.Where(e => e.TypeId == typeId);
            var eventsList = events.ToList();
            eventsList.Sort(new EventComparer());
            if (eventsList.Any())
            {
                List<EventListJson> eventsJson = _mapper.Map<List<EventListJson>>(eventsList);
                eventsJson.Sort(new EventJsonComparer());
                var data = eventsJson.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}