using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using bbom.Data.IdentityModel;
using bbom.Data.MetaData;

namespace bbom.Data.ContentModel
{
    [MetadataType(typeof(EventMetaData))]
    public partial class Event
    {
        public string SpikerId { get; set; }

        public ICollection<AspNetUser> Spikers
        {
            get
            {
                var users = DataFasade.GetRepository<AspNetUser>().GetAll().ToList();
                var es = EventSpikers;
                var usersIds = es.Select(spiker => spiker.SpikerId).ToList();
                var spikers = new List<AspNetUser>();
                foreach (var user in users)
                {
                    if (usersIds.Contains(user.Id))
                    {
                        spikers.Add(user);
                    }
                }
                return spikers;
            }
        }
        public AspNetUser AspNetUser => DataFasade.GetRepository<AspNetUser>().GetById(UserId);
    }
}