using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Enums;
using WalpaperChanger.Interfaces.Objects;

namespace WalpaperChanger.Interfaces
{
    public interface IPictureProvider
    {
        IList<IPicture> UsedPictures { get; }
        IList<IPicture> UnusedPictures { get; }

        void RefreshPictures(Category? category = null);
        void RefreshPictures(string query = null);
        void UsePicture(IPicture pic);
        IPicture GetPicture(Category? category = null);
    }
}
