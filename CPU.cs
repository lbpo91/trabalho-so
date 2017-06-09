using System;

namespace ProcessScheduler
{
	class CPU
	{
		private int quantum;
		private Process exeProcess;
		private Scheduler scheduler;
		private bool userMode;

		public CPU(Scheduler sch)
		{
			this.quantum = 0;
			this.exeProcess = null;
			this.scheduler = sch;
			this.userMode = true;
		}

        public void display()
        {
            if(exeProcess != null)
                Console.WriteLine("Executing process with #Id {0}: ", exeProcess.getId());
            else
                Console.WriteLine("No process executing.");
            if(userMode)
                Console.WriteLine("FLAG userMode is on.");
            else
                Console.WriteLine("FLAG userMode is off.");
        }

		public bool isOnUserMode()
		{
			return userMode;
		}

		public Process getExeProcess()
		{
			return exeProcess;
		}

		//Metodo que executa cada ciclo
		public void run()
		{
			if (quantum > 0)
			{
				quantum--;
                int remainingTime = exeProcess.getServiceTime() - 1;
                exeProcess.setServiceTime(remainingTime);
				if (quantum == 0 || remainingTime == 0)
				{
					scheduler.schedule(this, exeProcess, userMode);
				}
			}
			else
				scheduler.schedule(this);
		}

		public void allocateProcess(Process p, int q, bool mode)
		{
			this.exeProcess = p;
			this.quantum = q;
			this.userMode = mode;
		}
	}
}