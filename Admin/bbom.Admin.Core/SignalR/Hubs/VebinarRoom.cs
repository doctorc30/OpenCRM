using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bbom.Data;
using bbom.Data.ModelPartials.Constants;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace bbom.Admin.Core.SignalR.Hubs
{
    [HubName("Chat")]
    public class VebinarRoom : Hub
    {
        public class User
        {
            public string Name { get; set; } 
            public string FIO { get; set; } 
            public string ConnectionId { get; set; } 
            public int EventId { get; set; } 
            public string Adress { get; set; } 
        }
        public static List<User> Users = new List<User>();
        public static List<User> Visitors = new List<User>();
        static Dictionary<int, bool> _chatsStatus = new Dictionary<int, bool>();

        public static void UpdateStartBisnesButton(int evnId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<VebinarRoom>();
            var users = Users.Where(user => user.EventId == evnId);
            foreach (var user in users)
            {
                hubContext.Clients.Client(user.ConnectionId).updateStartBisnesButton();
            }
        }

        public void ChatStatus(int eventId, bool status)
        {
            var caller = Users.SingleOrDefault(user => user.ConnectionId == Context.ConnectionId);
            if (caller == null)
                return;
            var userBd = DataFasade.GetUserByName(caller.Name);
            if (userBd.AspNetRoles.All(role => role.Name != UserRole.Spiker))
                return;
            var users = Users.Where(user => user.EventId == eventId);
            if (_chatsStatus.ContainsKey(eventId))
            {
                _chatsStatus[eventId] = status;
            }
            else
            {
                _chatsStatus.Add(eventId, status);

            }
            foreach (var user in users)
            {
                if (status)
                {
                    Clients.Client(user.ConnectionId).chatOn();
                }
                else
                {
                    Clients.Client(user.ConnectionId).chatOff();
                }
                
            }
        }

        // Отправка сообщений
        public void Send(string fio, string id, string message, int eventId)
        {
            if (!_chatsStatus.ContainsKey(eventId))
            {
                _chatsStatus.Add(eventId, true);
            }
            if (!_chatsStatus[eventId])
                return;
            var users = Users.Where(user => user.EventId == eventId);
            var sender = Context.ConnectionId;
            foreach (var user in users)
            {
                if (user.ConnectionId == sender)
                {
                    continue;
                }
                Clients.Client(user.ConnectionId)
                    .addMessage(fio, GlobalConstants.ImageUserProfilePath + id, message);
            }
        }

        // Адресс пользователя
        public void SetUserAdress(string adress)
        {
            var id = Context.ConnectionId;
            var user = Users.FirstOrDefault(u => u.ConnectionId == id);
            if (user != null)
            {
                user.Adress = adress;
                Clients.All.updateUserAdress(user.Name, user.Adress);
            }
        }


        // Подключение нового пользователя
        public void Connect(string userFIO, string userName, int eventId)
        {
            var id = Context.ConnectionId;
            var newuser = new User { ConnectionId = id, Name = userName, EventId = eventId, FIO = userFIO };
            if (Users.All(user => user.Name != userName))
            {
                Users.Add(newuser);
                Visitors.Add(newuser);
            }
            var users = Users.Where(user => user.EventId == eventId).ToList();
            Clients.Caller.updateAllUsers(users.Where(user => user.EventId == eventId).ToList());
            Clients.Caller.updateChat(_chatsStatus.ContainsKey(eventId) && _chatsStatus[eventId]);
            foreach (var user in users)
            {
                Clients.Client(user.ConnectionId).updateIterator(users.Count);

            }
            Clients.Others.addUser(newuser.Name, newuser.FIO);
        }

        //Отключение пользователя
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var users = Users.Where(u => u.EventId == item.EventId);
                foreach (var user in users)
                {
                    Clients.Client(user.ConnectionId).updateIterator(Users.Count);
                    Clients.Client(user.ConnectionId).removeUser(item.Name);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}