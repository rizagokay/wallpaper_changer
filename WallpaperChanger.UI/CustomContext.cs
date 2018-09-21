using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperChanger.UI.Forms;
using WallpaperChanger.UI.Properties;
using WalpaperChanger.Enums;
using WalpaperChanger.Events;
using WalpaperChanger.Events.Messages;
using WalpaperChanger.Interfaces;
using WalpaperChanger.Interfaces.Objects;
using WalpaperChanger.JsonObjects;
using WalpaperChanger.Objects;

namespace WallpaperChanger.UI
{
    public class CustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private IWallPaperChanger _wp;
        private IPictureProvider _pw;
        private IFileManager _fm;
        private IPicture _currentPicture;
        private Category? _selectedCategory;
        private MenuItem _imageMenuItem;
        private MenuItem _pauseResumeItem;
        private MenuItem _customMenuItem;
        private System.Windows.Forms.Timer _wpTimer;

        public CustomApplicationContext()
        {
            //Subscrite to events
            EventAggregator.Instance.Subscribe<ExceptionMessage>(e =>
            {
                this.trayIcon.ShowBalloonTip(5000, "Error Occured", $"An error occured while setting new image: {e.Message}", ToolTipIcon.Error);

                _customMenuItem.Text = "Custom";
                _customMenuItem.Checked = false;

            });

            EventAggregator.Instance.Subscribe<InfoMessage>(e =>
            {
                this.trayIcon.ShowBalloonTip(5000, "Info", e.Message, ToolTipIcon.Info);
            });

            EventAggregator.Instance.Subscribe<SettingsChangedMessage>(e => {

                _wpTimer.Stop();
                _wpTimer.Interval = e.Runtime * 60 * 1000;
                _wpTimer.Start();

            });

            _pw = new UnSplashPictureProvider();

            _wp = new WallPaperChanger();

            _fm = new FileManager();

            _wpTimer = new System.Windows.Forms.Timer();

            var jsonData = _fm.GetProgramData();

            int defaultRuntime = 10;

            if (jsonData != null)
            {
                if (jsonData.Settings != null)
                {
                    if (jsonData.Settings.TimeInMinutes != 0)
                    {
                        defaultRuntime = jsonData.Settings.TimeInMinutes;
                    }
                }
            }

            //10 Minutes
            _wpTimer.Interval = defaultRuntime * 60 * 1000;

            //Bind Event
            _wpTimer.Tick += _wpTimer_Tick;

            _wpTimer.Start();



            _imageMenuItem = new MenuItem("Open Image", OpenImage_Click);
            _pauseResumeItem = new MenuItem("Pause", PauseOrStart);
            _customMenuItem = new MenuItem("Custom", Custom_Category_Click);

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                _imageMenuItem,
                new MenuItem("Next Wallpaper", ChangeWallpaper_Click),
                new MenuItem("Category", new MenuItem [] {
                new MenuItem("Architecture", Category_Click) {Tag = Category.Architecture },
                new MenuItem("Nature", Category_Click) {Tag = Category.Nature },
                new MenuItem("Night", Category_Click) {Tag = Category.Night },
                new MenuItem("City", Category_Click) {Tag = Category.City },
                new MenuItem("SkyScraper", Category_Click) {Tag = Category.SkyScraper },
                new MenuItem("Forest", Category_Click) {Tag = Category.Forest },
                new MenuItem("Animal", Category_Click) {Tag = Category.Animal },
                new MenuItem("Games", Category_Click) {Tag = Category.Games },
               _customMenuItem
                }),
               _pauseResumeItem,
                new MenuItem("Settings", Settings_Click),
                new MenuItem("About", About_Click),
                new MenuItem("Exit", Exit)


            }),
                Visible = true,
                Text = "Wallpaper Changer v0.2"
            };

            

            if (jsonData?.LastSelectedCategory != "")
            {
                ChangeWithQuery(jsonData.LastSelectedCategory, _customMenuItem);
            }
            else
            {
                ChangeWallPaper();
            }


            this.trayIcon.MouseClick += TrayIcon_MouseClick;

        }

        void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ChangeWallpaper_Click(sender, e);
            }
        }

        void About_Click(object sender, EventArgs e)
        {
            var abt = new AboutForm();
            abt.ShowDialog();
        }

        void Settings_Click(object sender, EventArgs e)
        {
            var stn = new SettingsForm();
            stn.ShowDialog();
        }

        void _wpTimer_Tick(object sender, EventArgs e)
        {
            ChangeWallPaper();
        }

        void PauseOrStart(object sender, EventArgs e)
        {
            var mItem = sender as MenuItem;

            if (this._wpTimer.Enabled)
            {
                mItem.Text = "Resume";

                this._wpTimer.Stop();
            }
            else
            {
                mItem.Text = "Pause";

                this._wpTimer.Start();
            }

        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }

        void OpenImage_Click(object sender, EventArgs e)
        {
            Process.Start(_currentPicture.Url);
        }

        void Category_Click(object sender, EventArgs e)
        {
            var mItem = sender as MenuItem;

            _selectedCategory = (Category)mItem.Tag;

            ClearSelections();

            mItem.Checked = true;

            CategoryChanged();
        }

        void Custom_Category_Click(object sender, EventArgs e)
        {

            var frm = new CustomCategoryForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.Query.Trim() != "")
                {
                    ChangeWithQuery(frm.Query, sender as MenuItem);

                    var jsonData = _fm.GetProgramData();

                    jsonData.LastSelectedCategory = frm.Query;

                    _fm.SaveJsonData(jsonData);
                }
            }

        }

        void ChangeWithQuery(string query, MenuItem item)
        {
            ClearSelections();

            item.Text = $"Custom ({query})";
            item.Checked = true;

            CategoryChanged(query);

            ChangeWallPaper();
        }

        void ClearSelections()
        {

            foreach (MenuItem item in this.trayIcon.ContextMenu.MenuItems)
            {
                foreach (MenuItem subItem in item.MenuItems)
                {
                    subItem.Checked = false;
                }
            }
        }

        void ChangeWallpaper_Click(object sender, EventArgs e)
        {
            _wpTimer.Stop();

            ChangeWallPaper();

            _wpTimer.Start(); _pauseResumeItem.Text = "Pause";
        }

        void CategoryChanged()
        {

            _pw.RefreshPictures(_selectedCategory);

            ChangeWallPaper();

        }

        void CategoryChanged(string query)
        {
            _pw.RefreshPictures(query);
        }

        void ChangeWallPaper()
        {
            try
            {
                var picture = _pw.GetPicture(_selectedCategory ?? null);

                _wp.SetWallpaper(new Uri(picture.DownloadLink), WalpaperChanger.Enums.Style.Stretched);

                _currentPicture = picture;

                _imageMenuItem.Text = $"Photo by: {picture.UserName} (Unsplash)";

            }
            catch (Exception Ex)
            {
                //Silence
                this.trayIcon.ShowBalloonTip(5000, "Error Occured", $"An error occured while setting new image: {Ex.Message}", ToolTipIcon.Error);
            }
        }
    }
}
