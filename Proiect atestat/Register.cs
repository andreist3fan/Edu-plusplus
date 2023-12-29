using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace Proiect_atestat
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private bool auth = true;
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
                textBox1.Text = "Prenume";
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Prenume")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = Color.Gray;
                textBox2.Text = "Email";
            }
            else if (textBox2.Text != "Email")
            {
                FirebaseResponse res = client.Get(@"Useri/" + emailtouser(textBox2.Text));
                Useri rezUser = res.ResultAs<Useri>();
                if (rezUser != null && rezUser.Email == textBox2.Text)
                {
                    MessageBox.Show("Email-ul este deja folosit");
                    auth = false;
                    return;
                }
            }
            else
                auth = true;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Email")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.PasswordChar = '\0';
                textBox3.ForeColor = Color.Gray;
                textBox3.Text = "Parolă";

            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Parolă")
            {
                textBox3.PasswordChar = '•';
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.PasswordChar = '\0';
            }
            else
                textBox3.PasswordChar = '•';
        }

        private void Register_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            #region Condition
            if ((textBox1.Text == "Username" && textBox2.ForeColor == Color.Gray) || (textBox2.Text == "Email" && textBox2.ForeColor == Color.Gray) || (textBox3.Text == "Password" && textBox3.ForeColor == Color.Gray) || comboBox1.SelectedItem.ToString() == "Selecteaza Scoala:")
            {
                MessageBox.Show("Aveti campuri necompletate");
                return;
            }
            else if (!textBox2.Text.Contains('@') || !textBox2.Text.Contains('.'))
            { MessageBox.Show("Emailul este invalid!"); return; }
            else if ((DateTime.Now.Year - dateTimePicker1.Value.Year > 23 || DateTime.Now.Year - dateTimePicker1.Value.Year<6) && no.Checked)
            {
                MessageBox.Show("Trebuie să aveți între 7 și 22 de ani pentru a vă crea un cont de elev");
                return;
            }
            else if ((DateTime.Now.Year - dateTimePicker1.Value.Year <18 || DateTime.Now.Year - dateTimePicker1.Value.Year > 80)  && yes.Checked)
            {
                MessageBox.Show("Trebuie să aveți între 18 și 80 de ani pentru a vă crea un cont de profesor");
                return;
            }




            #endregion
            Useri user = new Useri()
            {
                Nume = nume.Text,
                Prenume = textBox1.Text,
                Email = textBox2.Text,
                Parola = CreateMD5(textBox3.Text),
                Scoala = comboBox1.SelectedItem.ToString(),
                Profesor = yes.Checked,
                DataNasterii = dateTimePicker1.Value.Date.ToString("dd - MM - yyyy")

            };
            SetResponse set = client.Set(@"Useri/" + emailtouser(textBox2.Text), user);

            MessageBox.Show("Inregistrarea contului a fost realizata cu succes! Te vom redirectiona la pagina de login.", "Succes!");
            Login login = new Login();
            login.Show();
            this.Close();


        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void nume_TextChanged(object sender, EventArgs e)
        {

        }

        private void nume_Enter(object sender, EventArgs e)
        {
            if (nume.Text == "Nume")
            {
                nume.Text = "";
                nume.ForeColor = Color.Black;
            }
        }

        private void nume_Leave(object sender, EventArgs e)
        {
            if (nume.Text == "")
            {
                nume.ForeColor = Color.Gray;
                nume.Text = "Nume";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        
    }
}
