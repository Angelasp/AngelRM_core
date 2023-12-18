
namespace Angel.Service
{
    public interface IApplicationSettingService
    {
        object GetSettingValue(string settingKey);

        string SetSettingValue(string settingKey, object settingValue);

        int AddSettings(Dictionary<string, string> dictionary);
    }
}
