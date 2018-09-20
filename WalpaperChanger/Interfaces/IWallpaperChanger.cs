using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Enums;

namespace WalpaperChanger.Interfaces
{
    public interface IWallPaperChanger
    {
        void SetWallpaper(Uri url, Style style);
    }
}
