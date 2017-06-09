using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScheduler
{
    class Program
    {
        public const int PRINTERS = 2;
        public const int SCANNERS = 1;
        public const int MODEMS = 1;
        public const int CD_DRIVES = 2;
        public const int MEM_SIZE = 1024;

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Empty file path!");
                System.Environment.Exit(1);
            }

            string filename = args[0];
            int clocks = 1;
            bool status = false;


            // Processor clock time in seconds
            int clockTime = 1;

            //Inicializa elementos do sistema
            ResourceManager resMngr = new ResourceManager();

            MP mp = new MP();

            Scheduler scheduler = new Scheduler(resMngr, mp);

            Dispatcher dispatcher = new Dispatcher(scheduler, resMngr, filename);

            while(clocks > 0)
            {
                Console.Write("How many cicles(int value) do you want to run?(0 to end)  ");
                clocks = Int32.Parse(Console.ReadLine());
                Console.WriteLine();

                for (int i = 0; i < clocks; i++)
                {
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine("clockTime:  {0} ", clockTime);
                    Console.WriteLine();

                    resMngr.run();
                    scheduler.run();
                    dispatcher.run(clockTime);

                    clockTime++;
                }

                Console.WriteLine("Do you want to see the system status?(y/n)  ");
                string ans = Console.ReadLine();
                Console.WriteLine();

                if (ans.Equals("y", StringComparison.OrdinalIgnoreCase) || ans.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    status = true;
                else
                    status = false;

                if (status)
                {
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("-----------------System Status Begin--------------------");
                    scheduler.display();
                    mp.display();
                    resMngr.display();
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("------------------System Status End---------------------");
                    Console.WriteLine();
                }
            }
        }
    }
}
