using System;
using System.Collections.Generic;

namespace ProcessScheduler
{
	class Dispatcher
	{
		private List<Process> notArrived;
		private Queue<Process> newlyArrived;
		private Scheduler scheduler;
		private ResourceManager resMngr;

		public Dispatcher(Scheduler sch, ResourceManager rm, string filename)
		{
			this.notArrived = new List<Process>();
			this.newlyArrived = new Queue<Process>();
			this.scheduler = sch;
			this.resMngr = rm;

			populateNotArrived(filename);
		}

		//Comparador particular
		private static int CompareArrivalTime(Process x, Process y)
		{
			if (x == null)
			{
				if (y == null)
				{
			    	// If x is null and y is null, they're
			    	// equal. 
			    	return 0;
				}
				else
				{
			    	// If x is null and y is not null, y
			    	// is greater. 
			    	return -1;
				}
			}
			else
			{
				// If x is not null...
				//
				if (y == null)
			    	// ...and y is null, x is greater.
				{
		    	return 1;
				}
				else
				{
					// ...and y is not null, compare the 
                	// ArrivalTime of the two processes.
                	//
                	int xAT = x.getArrivalTime();
                	int yAT = y.getArrivalTime();
                	
                	return xAT.CompareTo(yAT);
				}
			}
		}

		private void populateNotArrived(string filename)
		{
			//Read each line of the file
			string[] lines = System.IO.File.ReadAllLines(filename);
			int id = 1;

			foreach(string line in lines)
			{
				string[] str = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

				Process p = new Process(id, Int32.Parse(str[0]), Int32.Parse(str[1]), 
                    Int32.Parse(str[2]), Int32.Parse(str[3]), Int32.Parse(str[4]), 
                    Int32.Parse(str[5]), Int32.Parse(str[6]), Int32.Parse(str[7]));

				notArrived.Add(p);
                id++;
			}

			notArrived.Sort(CompareArrivalTime);
		}

		public void run(int clockTime)
		{
			while(notArrived.Count > 0 && notArrived[0].getArrivalTime() == clockTime)
			{
				newlyArrived.Enqueue(notArrived[0]);
				notArrived[0].state = ProcessState.NEW;

                //Escreve dados do processo entrando no so
                displayNewProcessInfo(notArrived[0]);

                notArrived.RemoveAt(0);
            }

			while(newlyArrived.Count > 0)
			{
				Process p = newlyArrived.Dequeue();

				if(p.getPriority() == 0)
				{
					//ftr
					scheduler.scheduleFTR(p);
				}
				else
				{
					//feedback
					if(p.getResourceCount() > 0)
					{
						resMngr.requestResources(p);
                        scheduler.disableProcess(p);
						p.state = ProcessState.DISABLED;
						notifyChangeState(p.getId(), ProcessState.NEW, ProcessState.DISABLED);
					}
					else
					{
						scheduler.schNewUserProcess(p);	
					}
				}
			}
		}

		public static void notifyChangeState(int id, ProcessState prev, ProcessState curr)
		{
			Console.WriteLine("Process with Id# {0}  changed state: {1} -> {2}", id, prev, curr);
            Console.WriteLine();
        }

		private void displayNewProcessInfo(Process p)
		{
            Console.WriteLine("New process information: ");
            Console.WriteLine("          Id: {0}", p.getId());
            Console.WriteLine("    Priority: {0}", p.getPriority());
            Console.WriteLine("Service Time: {0}", p.getServiceTime());
            Console.WriteLine("      Mbytes: {0}", p.getMBytes());
            Console.WriteLine("   #Printers: {0}", p.getPrinters());
            Console.WriteLine("   #Scanners: {0}", p.getScanners());
            Console.WriteLine("     #Modems: {0}", p.getModems());
            Console.WriteLine("        #CDs: {0}", p.getCDDrives());
            Console.WriteLine();
        }
	}
}