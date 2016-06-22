using System;
using System.Collections.Generic;

namespace bbom.Admin.Core.ViewModels
{
    public class MenuJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string discription { get; set; }
        public string icon { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
        public bool isActive { get; set; }
        public int order { get; set; }
        public string exParam { get; set; }
        public Type destinationType { get; set; }
        public Type accessType { get; set; }
        public ICollection<MenuJson> SubMenusJsons { get; set; }
    }
}