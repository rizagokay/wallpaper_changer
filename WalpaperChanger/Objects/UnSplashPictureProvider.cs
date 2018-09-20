using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Enums;
using WalpaperChanger.Interfaces;
using WalpaperChanger.Interfaces.Objects;

namespace WalpaperChanger.Objects
{
    public class UnSplashPictureProvider : IPictureProvider
    {

        private IUnSplashService _srv;
        private IList<IPicture> _usedPictures;
        private IList<IPicture> _unUsedPictures;

        public UnSplashPictureProvider()
        {
            _srv = new UnSplashService("4ed7e8a52b7cbf4dc5042217b7539b042a4727620f4d7cd8429ed2cf1bec0cd6");

            _usedPictures = new List<IPicture>();

            _unUsedPictures = new List<IPicture>();

            RefreshPictures(query: null);
        }

        public IList<IPicture> UnusedPictures
        {
            get
            {
                return _unUsedPictures;

            }
        }

        public IList<IPicture> UsedPictures
        {
            get
            {
                return _usedPictures;

            }
        }

        public void RefreshPictures(Category? category = null)
        {
            this._usedPictures = new List<IPicture>();

            this._unUsedPictures = new List<IPicture>();

            this._unUsedPictures = _srv.GetRandomImages(category: category ?? null).ToList();
        }

        public void RefreshPictures(string query = null)
        {
            this._usedPictures = new List<IPicture>();

            this._unUsedPictures = new List<IPicture>();

            this._unUsedPictures = _srv.GetRandomImages(query: query ?? null).ToList();
        }

        public void UsePicture(IPicture pic)
        {
            var picF = this._unUsedPictures.Where(x => x.DownloadLink == pic.DownloadLink).FirstOrDefault();

            if (picF != null)
            {
                this._unUsedPictures.Remove(picF);
            }

            this._usedPictures.Add(picF);
        }

        public IPicture GetPicture(Category? category = null)
        {
            if (this.UsedPictures.Count == 30 || this.UnusedPictures.FirstOrDefault() == null)
            {
                this.RefreshPictures(category: category ?? null);
            }

            var p = this._unUsedPictures.First();


            this.UsePicture(p);

            return p;
        }
      
    }
}
