using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Proiect_atestat
{
    public partial class Login : Form
    {


        public Login()
        {
            InitializeComponent();
        }
        public static string user;
        public static string dire;
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string emailtouser(string a)
        {
            string s = a.Replace("@", "at");
            string p = s.Replace(".", "dot");
            return p;
        }
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "ETpF7RPhbAPA5r1Rr27K2UsnbzPz5KaXu5XmyRe1",
            BasePath = "https://users-edu-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Email";

            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Email")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.PasswordChar = '\0';
                textBox2.ForeColor = Color.Gray;
                textBox2.Text = "Parolă";

            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Parolă")
            {
                textBox2.PasswordChar = '•';
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Show();
        }
        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, e);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch
            {
                MessageBox.Show("Probleme cu conexiunea");
            }

        }

        private void button1_Click(object sender, EventArgs e)//login
        {
            #region Condition
            if ((textBox1.Text=="Email" && textBox1.ForeColor == Color.Gray) || (textBox2.Text=="Parolă" && textBox2.ForeColor==Color.Gray))
            {
                MessageBox.Show("Aveti campuri necompletate");
                return;
            }
            else if (!textBox1.Text.Contains("@") || !textBox1.Text.Contains("."))
            { MessageBox.Show("Emailul este invalid!"); return; }

            #endregion
            FirebaseResponse res = client.Get(@"Useri/" + emailtouser(textBox1.Text));
            Useri rezUser = res.ResultAs<Useri>();//rezultatul bazei de date
            Useri CurUser = new Useri()
            {
                Email = textBox1.Text,
                Parola = CreateMD5(textBox2.Text)
            };
            if (Useri.IsEqual(rezUser, CurUser))
            {
                #region Foldere
                dire = Environment.CurrentDirectory;
                dire = dire.Remove(dire.Length - 10);
                Console.WriteLine(dire);
               
                dire += @"\Lectii Fizica";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }

                dire = dire.Remove(dire.Length - 6);
                dire += "Matematica";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }
                dire = dire.Remove(dire.Length - 10);
                dire += "Chimie";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }
                dire = dire.Remove(dire.Length - 13);
                dire += "Fise de lucru Chimie";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }
                dire = dire.Remove(dire.Length - 6);
                dire += "Matematica";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }

                dire = dire.Remove(dire.Length - 10);
                dire += "Fizica";

                if (!Directory.Exists(dire))
                {
                    Directory.CreateDirectory(dire);
                }

                #endregion
                if (rezUser.Profesor == false)
                {
                    this.Hide();
                    user = rezUser.Nume + " " + rezUser.Prenume;
                    ElevPortal ep = new ElevPortal();
                    ep.Show();
                }
                else
                {
                    this.Hide();
                    user = rezUser.Nume + " " + rezUser.Prenume;
                    ProfesorPortal pp = new ProfesorPortal();
                    pp.Show();
                }


            }
            else
            {
                Useri.ShowError();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
                textBox2.PasswordChar = '•';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        /*private void upload_img_button_Click(object sender, EventArgs e)
        {
            String image_location = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    image_location = dialog.FileName;
                    req_image_box.ImageLocation = image_location;
                }


            }
            catch (Exception) { }
        }

        private void upload_fb_button_Click(object sender, EventArgs e)
        {
            FirebaseResponse res = client.Get(@"poze/" + req_image_box.ImageLocation);
            poze rezUser = res.ResultAs<Useri>();//rezultatul bazei de date
            Useri CurUser = new Useri()
            {
                Username = textBox1.Text,
                Parola = CreateMD5(textBox2.Text)
            };
        }*/
    }
}
