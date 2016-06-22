using System.Collections.Generic;

namespace bbom.Data.ModelPartials
{
    public class SettingEx
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ICollection<string> Values { get; set; }
    }
}