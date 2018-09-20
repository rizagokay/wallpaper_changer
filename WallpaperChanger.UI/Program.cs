using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool createdNew = true;

            using (Mutex mutex = new Mutex(true, "WallpaperChanger.UI", out createdNew))
            {
                if (createdNew)
                {
                    Application.ThreadException += new ThreadExceptionEventHandler(HandleAllExceptions);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new CustomApplicationContext());
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            SetForegroundWindow(process.MainWindowHandle);                         
                            break;
                        }
                    }
                }
            }
        }
    }
}
