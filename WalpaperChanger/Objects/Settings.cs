using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces.Objects;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.Objects
{
    public class Settings : ISettings
    {
        private int _timeInMinutes;
        private bool _startsWithWindows;

        public Settings(JsonSettings settings)
        {
            _timeInMinutes = settings.TimeInMinutes;
            _startsWithWindows = settings.StartsWithWindows;
        }

        public Settings(bool startsWithSystem, int timeInMinutes)
        {
            _timeInMinutes = timeInMinutes;
            _startsWithWindows = startsWithSystem;
        }


        public bool StartsWithSystem
        {
            get
            {
                return _startsWithWindows;
            }
        }

        public int TimeInMinutes
        {
            get
            {
                return _timeInMinutes;
            }
        }
    }
}
