using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessScheduler
{
    static class Program
    {
        public const int PRINTERS = 2;
        public const int SCANNERS = 1;
        public const int MODEMS = 1;
        public const int CD_DRIVES = 2;
        public const int MEM_SIZE = 1024;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string filename = args[0];
            // Processor clock time in miliseconds
            int clockTime = 1;

            //Inicializa elementos do sistema
            ResourceManager resMngr = new ResourceManager();

            MP mp = new MP();

            Scheduler scheduler = new Scheduler(resMngr, mp);


            Dispatcher dispatcher = new Dispatcher(scheduler, resMngr, filename);

            Console.WriteLine("clockTime:  {0} ", clockTime);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
