using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Events.Messages
{
    public class InfoMessage : IApplicationEvent
    {
        public string Message { get; private set; }

        public InfoMessage(string Message)
        {
            this.Message = Message;
        }
    }
}
