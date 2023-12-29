using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_atestat
{
    class poze
    {
        System.Drawing.Image poza { get; set; }

        private static string error = "Utilizatorul nu exista!";

        public static void ShowError()
        {
            System.Windows.Forms.MessageBox.Show(error);
        }

    }

}
