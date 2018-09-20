using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Enums;
using WalpaperChanger.Interfaces.Objects;

namespace WalpaperChanger.Interfaces
{
    public interface IService
    {
        IPicture GetRandomImage();
        IEnumerable<IPicture> GetRandomImages(int imageCount = 30, Category? category = null);

        IEnumerable<IPicture> GetRandomImages(int imageCount = 30, string query=null);
    }
}
