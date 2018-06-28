using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tri_Bahtinov_Grabber_Autofocus
{
    public partial class Help : Form
    {
        public Help()
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
            richTextBox1.BackColor = SystemColors.Control;
        }

        private void NightMode_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Red;
            richTextBox1.BackColor = System.Drawing.Color.Red;
        }
    }

   
}
