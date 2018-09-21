using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalpaperChanger.Events.Messages
{
    public class SettingsChangedMessage : IApplicationEvent
    {
        public int Runtime { get; private set; }

        public SettingsChangedMessage(int Runtime)
        {
            this.Runtime = Runtime;
        }
    }
}
