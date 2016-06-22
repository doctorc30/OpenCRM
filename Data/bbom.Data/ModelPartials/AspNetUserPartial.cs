//using System.Collections.Generic;

//namespace bbom.Data.Model
//{
//    public partial class AspNetUser
//    {
//        public AspNetUser InvitedAspNetUser
//        {
//            get { return AspNetUser2 ?? this; }
//            set { AspNetUser2 = value; }
//        }

//        public ICollection<AspNetUser> InvitedAspNetUsers
//        {
//            get { return AspNetUsers11; }
//        }

//        public AspNetUser ParentAspNetUser => AspNetUser1;

//        public ICollection<AspNetUser> ChildrenAspNetUsers => AspNetUsers1;

//        public ICollection<ExtraRegParam> ReceivedExtraRegParams
//        {
//            get { return ExtraRegParams; }
//        }

//        public Template ActiveTemplate
//        {
//            get { return Template; }
//            set { Template = value; }
//        }

//        public string GetFio()
//        {
//            return Suname + " " + Name + " " + Altname;
//        }

//        public string GetIO()
//        {
//            return Name + " " + Suname;
//        }

//        public ICollection<Discount> ReceiveDiscounts => Discounts;
//        public ICollection<Event> SpikEvents => Events1;
//        public ICollection<Event> CreateEvents => Events;
//    }
//}