using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tri_Bahtinov_Grabber_Autofocus
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            GetSettingsValues();
        }

        private void GetSettingsValues()
        {
            try
            {
                this.RegularMode.Checked = Convert.ToBoolean(Properties.Settings.Default.Regular);
                this.NightMode.Checked = Convert.ToBoolean(Properties.Settings.Default.Night);

            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
                Properties.Settings.Default.Regular = RegularMode.Checked;
                Properties.Settings.Default.Night =  NightMode.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void RegularMode_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

        private void NightMode_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Red;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/cytan299/tribahtinov");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.cloudynights.com/topic/536410-a-tri-bahtinov-mask-for-sct-collimation-and-focusing/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/wytsep/bahtinov-grabber");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://web.archive.org/web/20160220123031/http://www.njnoordhoek.com:80/?cat=10");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/1CM69/Tri-Bahtinov_Grabber");
        }
    }
}
