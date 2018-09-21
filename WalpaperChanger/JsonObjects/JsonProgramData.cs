using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.JsonObjects
{
    public class JsonProgramData
    {
        public string LastSelectedCategory { get; set; }

        public JsonSettings Settings { get; set; }
    }
}
