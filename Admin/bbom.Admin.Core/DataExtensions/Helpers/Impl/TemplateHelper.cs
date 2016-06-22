using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class TemplateHelper : ITemplateHelper
    {
        public ICollection<SettingEx> GetTemplateSettings(Template template, AspNetUser user)
        {
            var templSettings = user.UsersTemplateSettings.Where(setting => setting.Template == template);
            return templSettings.Select(templSetting => new SettingEx
            {
                Name = templSetting.Setting.Name, Value = templSetting.Value
            }).ToList();
        }

        public SettingEx GetTemplateSetting(Template template, AspNetUser user, string settingName)
        {
            var settings = GetTemplateSettings(template, user);
            var setting = settings.SingleOrDefault(s => s.Name == settingName);
            if (setting == null)
            {
                setting = new SettingEx();
                var settingBd = DataFasade.GetRepository<Setting>().GetAll().Single(s => s.Name == settingName);
                var defaultSettingsValue = settingBd.DefaultSettingsValues.FirstOrDefault();
                if (defaultSettingsValue != null)
                    setting.Value = defaultSettingsValue.Value;
            }
            return setting;
        }

        public void SetTemplateSetting(Template template, AspNetUser user, string settingName, string value)
        {
            var templSettings = user.UsersTemplateSettings.Where(s => s.Template == template);
            var setting = DataFasade.GetRepository<Setting>().GetAll().Single(s => s.Name == settingName);
            var oldSettings = templSettings.Where(s => s.Setting.Name == settingName);
            foreach (var templSetting in oldSettings)
            {
                DataFasade.GetRepository<UsersTemplateSetting>().Delete(templSetting);
            }
            DataFasade.GetRepository<UsersTemplateSetting>().Insert(new UsersTemplateSetting
            {
                Template = template,
                UserId = user.Id,
                SettingId = setting.Id,
                Value = value,
                Setting = setting
            });
            DataFasade.GetRepository<UsersTemplateSetting>().SaveChanges();
        }

        public object GetTemplatesJson(AspNetUser user)
        {
            var defaultTemplates = DataFasade.GetRepository<Template>().GetAll().Where(template => template.IsDefault == 1);
            var defaultObjects = new List<object>();
            foreach (var template in defaultTemplates)
            {
                var dataObjects = new List<string> { template.Id.ToString(), template.Name };
                defaultObjects.Add(dataObjects.ToArray());
            }
            var createdTemplates = user.UsersTemplateSettings.Select(setting => setting.Template);
            var createdObjects =
                createdTemplates.Select(template => new List<string> { template.Id.ToString(), template.Name })
                    .Select(dataObjects => dataObjects.ToArray())
                    .Cast<object>()
                    .ToList();
            return new { defaults = defaultObjects, created = createdObjects };
        }
    }
}