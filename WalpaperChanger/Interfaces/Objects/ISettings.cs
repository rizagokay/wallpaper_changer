using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Interfaces.Objects
{
    public interface ISettings
    {
        int TimeInMinutes { get; }
        bool StartsWithSystem { get; }
    }
}
