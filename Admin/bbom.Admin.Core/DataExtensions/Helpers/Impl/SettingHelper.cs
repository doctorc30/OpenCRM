using System;
using System.Linq;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.ModelPartials;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class SettingHelper : ISettingsHelper
    {
        public SettingEx GetSetting(string settingType)
        {
            var setting = DataFasade.GetRepository<Setting>().GetAll().SingleOrDefault(s => s.Name == settingType);
            var defaultSettingsValue = setting?.DefaultSettingsValues.FirstOrDefault();
            var setEx = new SettingEx { Name = settingType };
            if (defaultSettingsValue != null)
            {
                setEx = new SettingEx
                {
                    Value = defaultSettingsValue.Value,
                    Values = setting.DefaultSettingsValues.Select(value => value.Value).ToList()
                };
            }
            return setEx;
        }

        public void SetSetting(SettingEx settingEx)
        {
            var setting = DataFasade.GetRepository<Setting>().GetAll().SingleOrDefault(s => s.Name == settingEx.Name);
            if (setting != null)
            {
                if (settingEx.Value != null && !settingEx.Values.Contains(settingEx.Value))
                {
                    settingEx.Values.Add(settingEx.Value);
                }
                var strValues = setting.DefaultSettingsValues.Select(s => s.Value).ToList();
                foreach (var value in settingEx.Values)
                {
                    if (!strValues.Contains(value))
                    {
                        setting.DefaultSettingsValues.Add(new DefaultSettingsValue
                        {
                            Setting = setting,
                            Value = value,
                            SettingId = setting.Id
                        });
                    }
                }
                DataFasade.GetRepository<Setting>().SaveChanges();
            }
            throw new Exception("Настройки не существует!");
        }
    }
}