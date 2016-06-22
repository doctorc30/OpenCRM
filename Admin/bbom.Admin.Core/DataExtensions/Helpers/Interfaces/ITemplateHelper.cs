using System.Collections.Generic;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface ITemplateHelper
    {
        ICollection<SettingEx> GetTemplateSettings(Template template, AspNetUser user);

        SettingEx GetTemplateSetting(Template template, AspNetUser user, string settingName);

        void SetTemplateSetting(Template template, AspNetUser user, string settingName, string value);

        object GetTemplatesJson(AspNetUser user);
    }
}