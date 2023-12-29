using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Proiect_atestat
{
    public partial class ElevPortal : Form
    {
        public ElevPortal()
        {
            InitializeComponent();
        }

        private void ElevPortal_Load(object sender, EventArgs e)
        {
            /*GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(button1.Height, 0, button1.Height, button1.Height);
            button1.Region = new Region(gp);
            gp.Dispose();*/
            label1.Text = Login.user;
            SaveButton.Hide();
        }
        public static string path;
        public static bool ok= false ;
        private void button1_Click(object sender, EventArgs e)
        {
            path = Login.dire.Remove(Login.dire.Length - 20);
            openFileDialog1.Title = "Browse Text Files";
            openFileDialog1.DefaultExt = "rtf";
            openFileDialog1.Filter = "Rich Text Files (*.rtf)|*.rtf|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (!ok)
                path+="Lectii ";
            else
                path += "Fise de lucru ";
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            { MessageBox.Show("Selectati o materie"); return; } 
            else if (radioButton3.Checked)
                path += radioButton3.Text;
            else if (radioButton2.Checked)
                path += radioButton2.Text;
            else if (radioButton1.Checked)
                path += radioButton1.Text;

            if (Directory.Exists(path))
            {
                openFileDialog1.InitialDirectory = path;
                
            }
            else
            {
                openFileDialog1.InitialDirectory = @"C:\";
            }
            
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                
                string file = openFileDialog1.FileName;
                richTextBox1.Show();
                ExitButton.Show();
                richTextBox1.LoadFile(openFileDialog1.FileName);
                if (ok == true)
                {
                    SaveButton.Show();
                    richTextBox1.ReadOnly = false;
                    path = openFileDialog1.FileName;
                    path = path.Remove(path.Length - 4);
                    path += "_Results";
                    System.Diagnostics.Debug.WriteLine(path);
                }
                else
                    richTextBox1.ReadOnly = true;
            }

       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Hide();
            ExitButton.Hide();
            SaveButton.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void ModTestBox_CheckedChanged(object sender, EventArgs e)
        {
            if(ModTestBox.Checked)
                ok = true;
            
            else
                ok = false;
  
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = path;
            saveFileDialog1.FileName = Login.user;
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.DefaultExt = "*.rtf";
            saveFileDialog1.Filter = "Rich Text Files (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog1.OverwritePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            button2_Click(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
