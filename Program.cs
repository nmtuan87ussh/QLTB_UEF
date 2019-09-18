using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTB_UEF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new FormLogIn().ShowDialog();
            //if ((new FormLogIn()).ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new Form1());
            //}
        }
    }
}
