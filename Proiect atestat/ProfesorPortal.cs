using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_atestat
{
    public partial class ProfesorPortal : Form
    {
       
        public ProfesorPortal()
        {
            InitializeComponent();
        }

        public static string materie;
        public static bool test = false;

        private void button1_Click(object sender, EventArgs e)
        {
            materie = Login.dire.Remove(Login.dire.Length - 20);
            if (ModTestBox.Checked)
            { materie += "Fise de lucru "; test = true; }
            else
            { materie += "Lectii "; test = false; }

            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            { MessageBox.Show("Selectati o materie"); return; }


            else if (radioButton3.Checked)
                materie += radioButton3.Text;
            else if (radioButton2.Checked)
                materie += radioButton2.Text;
            else if (radioButton1.Checked)
                materie += radioButton1.Text;
            this.Hide();
            Editor_text et = new Editor_text();
            et.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
        }

        private void ProfesorPortal_Load(object sender, EventArgs e)
        {
            label1.Text = Login.user;
        }

        private void ModTestBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
