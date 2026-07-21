using AnyStore.UI;
using System;
using System.Windows.Forms;

namespace AnyStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // .NET 8 Windows Forms initialization
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}
