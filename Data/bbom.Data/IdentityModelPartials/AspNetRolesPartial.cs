using System.Collections.Generic;
using System.Linq;
using bbom.Data.ContentModel;

namespace bbom.Data.IdentityModel
{
    public partial class AspNetRole
    {
        public object Tags { get; set; }
        public bool IsSelected { get; set; }

        public ICollection<Menu> AccessToMenu
            =>
                DataFasade.GetRepository<AccessToMenu>()
                    .GetAll()
                    .Where(menu => menu.RoleId == Id)
                    .Select(menu => menu.Menu).ToList();

        public ICollection<EventType> AccessToEventType 
            => 
            DataFasade.GetRepository<AccessToEventType>()
            .GetAll()
            .Where(type => type.RoleId == Id)
            .Select(type => type.EventType).ToList();
        public ICollection<DiscountType> AccessToDiscountType => DiscountTypes;
    }
}