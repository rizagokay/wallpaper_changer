using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WalpaperChanger.Events;
using WalpaperChanger.Events.Messages;
using WalpaperChanger.Interfaces;
using WalpaperChanger.Objects;

namespace WallpaperChanger.UI.Forms
{


    public partial class SettingsForm : Form
    {

        private ISettingsManager _stngs;
        private IRegistryManager _rgm;

        private int parsed;

        public SettingsForm()
        {
            InitializeComponent();

            textBox1.Text = "1";

            _stngs = new SettingsManager();
            _rgm = new RegistryManager();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out parsed))
            {
                textBox1.Text = "1";
                parsed = 1;
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool starts = checkBox1.Checked;
            int timeInMinutes = parsed;

            var stngs = new Settings(starts, timeInMinutes);

            _stngs.SaveSettings(stngs);

            _rgm.SetStartupKey(starts, Application.ExecutablePath);

            EventAggregator.Instance.Publish(new SettingsChangedMessage(timeInMinutes));
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var settings = _stngs.GetSettings();

            this.checkBox1.Checked = settings.StartsWithSystem;
            this.textBox1.Text = settings.TimeInMinutes.ToString();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
