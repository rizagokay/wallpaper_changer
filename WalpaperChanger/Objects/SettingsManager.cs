using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces;
using WalpaperChanger.Interfaces.Objects;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.Objects
{
    public class SettingsManager : ISettingsManager
    {
        private IFileManager _fm;

        public SettingsManager()
        {
            _fm = new FileManager();
        }

        public ISettings GetSettings()
        {
            var json = _fm.GetProgramData();

            if (json != null)
            {
                if (json.Settings != null)
                {
                    if (json.Settings.TimeInMinutes != 0)
                    {
                        return new Settings(json.Settings.StartsWithWindows, json.Settings.TimeInMinutes);
                    }
                }
            }

            return new Settings(false, 5);
        }

        public void SaveSettings(ISettings settings)
        {

            var data = _fm.GetProgramData();

            data.Settings = new JsonSettings { StartsWithWindows = settings.StartsWithSystem, TimeInMinutes = settings.TimeInMinutes };

            _fm.SaveJsonData(data);
        }
    }
}
