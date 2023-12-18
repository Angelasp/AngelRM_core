using System.Collections.Concurrent;

namespace Angel.Service
{
    public class ApplicationSettingInMemoryService : IApplicationSettingService
    {
        private readonly ConcurrentDictionary<string, object> _settingDictionary = new ConcurrentDictionary<string, object>();

        public int AddSettings(Dictionary<string, string> dictionary)
        {
            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var item in dictionary)
                {
                    _settingDictionary[item.Key] = item.Value;
                }
            }
            return _settingDictionary.Count;
        }

        public object GetSettingValue(string settingKey)
        {
            _settingDictionary.TryGetValue(settingKey, out var val);
            return val;
        }

        public string SetSettingValue(string settingKey, object settingValue)
        {
            _settingDictionary[settingKey] = settingValue;
            return settingKey;
        }
    }
}
