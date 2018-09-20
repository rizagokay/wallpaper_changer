using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Interfaces.Objects
{
    public interface IPicture
    {
        string DownloadLink { get; }
        string Url { get; }
        string UserName { get;  }
    }
}
