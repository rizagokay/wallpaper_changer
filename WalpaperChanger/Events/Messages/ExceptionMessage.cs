using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Events.Messages
{
    public class ExceptionMessage : IApplicationEvent
    {
        public string Message { get; private set; }

        public ExceptionMessage(string Message)
        {
            this.Message = Message;
        }
    }
}
