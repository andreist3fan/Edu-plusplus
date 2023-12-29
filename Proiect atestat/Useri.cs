using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_atestat
{
    class Useri
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string Scoala { get; set; }
        public string DataNasterii {get; set;}
        public bool Profesor { get; set; }
        //public int Fiz_Progress { get; set; }

        private static string error = "Utilizatorul nu exista!";

        public static void ShowError()
        {
            System.Windows.Forms.MessageBox.Show(error);
        }

        public static bool IsEqual(Useri user1, Useri user2)
        {
            if (user1 == null || user2 == null) { return false; }

            if (user1.Email != user2.Email)
            {
                error = "Utilizatorul nu exista!";
                return false;
            }

            else if (user1.Parola != user2.Parola)
            {
                error = "Parola este gresita!";
                return false;
            }

            return true;
        }
    }

}
