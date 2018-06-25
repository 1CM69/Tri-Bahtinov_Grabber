using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bahtinov_grabber_autofocus
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            GetRegistryValues();
        }

        private void GetRegistryValues()
        {
            try
            {

                this.RegularMode.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("RegularMode"));
                this.NightMode.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("NightMode"));

            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
                Application.UserAppDataRegistry.SetValue("RegularMode", (object)this.RegularMode.Checked);
                Application.UserAppDataRegistry.SetValue("NightMode", (object)this.NightMode.Checked);
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

        
    }
}
