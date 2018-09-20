using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.JsonObjects
{
    public class UnSplashResponse
    {
        public Links links { get; set; }
        public User user { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string html { get; set; }
        public string download { get; set; }
        public string download_location { get; set; }
    }

    public class User
    {
        public string name { get; set; }
    }
}
