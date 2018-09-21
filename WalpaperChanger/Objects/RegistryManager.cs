using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces;

namespace WalpaperChanger.Objects
{
    public class RegistryManager : IRegistryManager
    {
        public void SetStartupKey(bool startsWithSystem, string path)
        {
            var appName = "WallpaperChanger";

            RegistryKey rk = Registry.CurrentUser.OpenSubKey
             ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (startsWithSystem)
                rk.SetValue(appName, path);
            else
                rk.DeleteValue(appName, false);
        }
    }
}
