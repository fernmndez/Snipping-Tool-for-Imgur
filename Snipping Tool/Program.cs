using System;

namespace Snipping_Tool
{
    class Program
    {
        /// <summary>
        /// Program entry point, creates main window then shows it.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var mainWindow = new Application.MainWindow();
            mainWindow.ShowDialog();
        }

    }
}
