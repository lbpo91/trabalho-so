using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessScheduler
{
    static class Program
    {
        public static const int PRINTERS = 2;
        public static const int SCANNERS = 1;
        public static const int MODEMS = 1;
        public static const int CD_DRIVES = 2;
        public static const int MEM_SIZE = 1024;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Processor clock time in miliseconds
            int clockTime;

            ConcurrentQueue<Process> processQueue;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
