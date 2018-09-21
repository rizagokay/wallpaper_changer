using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces.Objects;

namespace WalpaperChanger.Interfaces
{
    public interface ISettingsManager
    {
         ISettings GetSettings();
        void SaveSettings(ISettings settings);
    }
}
