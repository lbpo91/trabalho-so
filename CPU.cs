namespace ProcessScheduler
{
	class CPU
	{
		private int quantum;
		private Process exeProcess;
		private bool userMode;
		private SchRealTime schRealTime;
		private SchFeedback schFeedback;

		public CPU(SchRealTime schR, SchFeedback schF)
		{
			quantum = 0;
			exeProcess = null;
			schRealTime = schR;
			schFeedback = schF;
			userMode = false;
		}

		//Metodo que executa cada ciclo
		public void run()
		{
			if (quantum > 0)
			{
				quantum--;
				exeProcess.run();
				if (quantum == 0)
				{
					if(!userMode)
						schRealTime.schedule(this, exeProcess);
					else
						schFeedback.schedule(this, exeProcess);
				}
			}
			else
				//Avisa que esta ocioso
		}

		public void allocateProcess(Process p, 
			int q, bool userMode)
		{
			exeProcess = p;
			quantum = q;
			this.userMode = userMode;
		}
	}
}