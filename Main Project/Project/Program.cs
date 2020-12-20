using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Interfaces;

namespace Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public const string ConPath = @"Data Source=DESKTOP-NFNAMHL\SQLEXPRESS;Initial Catalog=RailwayDatabase;Integrated Security=True"; 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
