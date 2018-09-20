using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WalpaperChanger.Events;
using WalpaperChanger.Events.Messages;

namespace WallpaperChanger.UI
{
    static class Program
    {

        private static void HandleAllExceptions(object sender, ThreadExceptionEventArgs t)
        {
            EventAggregator.Instance.Publish(new ExceptionMessage(t.Exception.Message));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.ThreadException += new ThreadExceptionEventHandler(HandleAllExceptions);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CustomApplicationContext());
        }
    }
}
