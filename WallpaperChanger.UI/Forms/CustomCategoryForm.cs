using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperChanger.UI.Forms
{
    public partial class CustomCategoryForm : Form
    {

        private string _query;

        public string Query { get { return _query; } }

        public CustomCategoryForm()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this._query = txt_Query.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
