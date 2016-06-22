using System.Collections.Generic;
using System.Linq;
using bbom.Data.ContentModel;

namespace bbom.Data.IdentityModel
{
    public partial class AspNetUser
    {
        public AspNetUser InvitedAspNetUser
        {
            get { return AspNetUser2 ?? this; }
            set { AspNetUser2 = value; }
        }

        public ICollection<AspNetUser> InvitedAspNetUsers
        {
            get { return AspNetUsers11; }
        }

        public AspNetUser ParentAspNetUser => AspNetUser1;

        public ICollection<AspNetUser> ChildrenAspNetUsers => AspNetUsers1;

        public ICollection<ExtraRegParam> ReceivedExtraRegParams
        {
            get
            {
                return
                    DataFasade.GetRepository<ReceivedExtraRegParam>()
                        .GetAll()
                        .Where(param => param.UserId == this.Id)
                        .Select(param => param.ExtraRegParam).ToList();
            }
        }

        public string GetFio()
        {
            return Suname + " " + Name + " " + Altname;
        }

        public string GetIO()
        {
            return Name + " " + Suname;
        }

        public string GetFullName()
        {
            return GetIO() + $" ({UserName})";
        }

        public ICollection<Discount> ReceiveDiscounts => Discounts;

        public ICollection<Event> SpikEvents
            =>
                DataFasade.GetRepository<EventSpiker>()
                    .GetAll()
                    .Where(es => es.SpikerId == Id)
                    .Select(es => es.Event)
                    .ToList();

        public ICollection<Event> CreateEvents
            => DataFasade.GetRepository<Event>().GetAll().Where(e => e.UserId == Id).ToList();

        public ICollection<UserExtraRegParam> UserExtraRegParams
            => DataFasade.GetRepository<UserExtraRegParam>().GetAll().Where(uerp => uerp.UserId == Id).ToList();

        public ICollection<TaskToDo> TaskToDoes
            =>
                DataFasade.GetRepository<UserComplitedTask>()
                    .GetAll()
                    .Where(ut => ut.UserId == Id)
                    .Select(ut => ut.TaskToDo).ToList();

        public ICollection<UsersTemplateSetting> UsersTemplateSettings
            => DataFasade.GetRepository<UsersTemplateSetting>().GetAll().Where(uts => uts.UserId == Id).ToList();
    }
}