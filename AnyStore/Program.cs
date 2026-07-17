using AnyStore.UI;
using System;
using System.Windows.Forms;

namespace AnyStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Updated for .NET 8: Application.SetHighDpiMode added for proper DPI handling.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // .NET 8 recommended: Configure DPI awareness before any UI initialization
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
