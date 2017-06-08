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
			this.userMode = false;
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
				exeProcess.setServiceTime(exeProcess.getServiceTime() - 1);
				if (quantum == 0)
				{
					scheduler.schedule(this, exeProcess, userMode);
					userMode = false;
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