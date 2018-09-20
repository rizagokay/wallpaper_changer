using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces.Objects;

namespace WalpaperChanger.Objects
{
    public class UnSplashPicture : IPicture
    {
        private string _downloadUrl;
        private string _url;
        private string _name;
        public UnSplashPicture(string DownloadUrl)
        {
            _downloadUrl = DownloadUrl;
        }

        public UnSplashPicture(string DownloadUrl, string Url)
        {
            _downloadUrl = DownloadUrl;
            _url = Url;
        }

        public UnSplashPicture(string DownloadUrl, string Url, string UserName)
        {
            _downloadUrl = DownloadUrl;
            _url = Url;
            _name = UserName;
        }

        public string DownloadLink
        {
            get
            {
                return _downloadUrl;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }

        public string UserName
        {
            get
            {
                return _name;
            }
        }
    }
}
