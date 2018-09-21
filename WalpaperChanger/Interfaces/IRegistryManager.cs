using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Interfaces
{
    public interface IRegistryManager
    {
        void SetStartupKey(bool startsWithSystem, string path);
    }
}
