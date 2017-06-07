namespace ProcessScheduler
{
	class CPU
	{
		private int quantum;
		private Process exeProcess;
		private Scheduler scheduler;

		public CPU(Scheduler sch)
		{
			this.quantum = 0;
			this.exeProcess = null;
			this.scheduler = scheduler;
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
					scheduler.schedule(this, exeProcess);
				}
			}
			else
				scheduler.schedule(this);
		}

		public void allocateProcess(Process p, int q)
		{
			exeProcess = p;
			quantum = q;
		}
	}
}