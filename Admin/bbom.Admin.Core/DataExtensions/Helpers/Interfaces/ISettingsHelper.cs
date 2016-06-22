using bbom.Data.ModelPartials;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface ISettingsHelper
    {
        SettingEx GetSetting(string settingType);
        void SetSetting(SettingEx setting);
    }
}