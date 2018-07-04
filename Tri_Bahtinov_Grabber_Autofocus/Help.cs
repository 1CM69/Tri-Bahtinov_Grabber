using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tri_Bahtinov_Grabber_Autofocus
{
    public partial class Help : Form
    {
        public Help()
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
                Properties.Settings.Default.Night = NightMode.Checked;
                Properties.Settings.Default.Save();
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
