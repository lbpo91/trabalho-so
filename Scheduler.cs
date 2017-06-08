using System;
using System.Collections.Generic;

namespace ProcessScheduler
{
	class Scheduler
	{
		private Queue<Process> ftr;
		private Queue<Process>[] readyQueues;
		private List<Process> disabled;
		private Queue<Process> suspended;
		private Queue<Process> ftrSuspended;
		private CPU[] arrayCPU;
		private ResourceManager resMngr;
		private MP mp;

		public Scheduler(ResourceManager rm, MP mem)
		{
			this.ftr = new Queue<Process>();
			this.readyQueues = new Queue<Process>[3];
            for(int i = 0; i < 3; i++)
			{
				readyQueues[i] = new Queue<Process>();
			}
            this.disabled = new List<Process>();
            this.suspended = new Queue<Process>();
            this.ftrSuspended = new Queue<Process>();
            this.arrayCPU = new CPU[4];
            for(int i = 0; i < 4; i++)
            {
                arrayCPU[i] = new CPU(this);
            }
			this.resMngr = rm;
			this.mp = mem;
		}

		//
		public void run()
		{
			//Verifica se processos impossibilitados já podem ficar prontos
			for(int i = disabled.Count - 1; i >= 0; i--)
			{
				Process p = disabled[i];

				//ResourceCount será 0 qnd o processo 
				//tiver todos os seus recursos reservados
				if(p.getResourceCount() == 0)
				{
					if(mp.allocate(p))//tem memória
					{
						readyQueues[p.getPriority() - 1].Enqueue(p);
						p.state = ProcessState.READY;
					}
					else //não tem memória
					{
						suspended.Enqueue(p);
						p.state = ProcessState.SUSPENDED;
					}
					
					Dispatcher.displayNewProcessInfo(p);

					disabled.RemoveAt(i);
				}
			}

			while(ftrSuspended.Count > 0 && mp.allocate(ftrSuspended.Peek()))
			{
				scheduleFTR(ftrSuspended.Dequeue());
			}

			if(ftrSuspended.Count == 0){
				while(suspended.Count > 0 && mp.allocate(suspended.Peek()))
				{
					Process p = suspended.Dequeue();
					readyQueues[p.getPriority() - 1].Enqueue(p);
					p.state = ProcessState.READY;
				}
			}
		}

		//Schedule para FTR que não se encontra na MP
		public void scheduleFTR(Process p)
		{
			if(mp.allocate(p))
			{
				bool noCPUfound = true;
				int i = 0;
				//percorre cpus que tem processo de usuario
				while(noCPUfound && i < 4)
				{
					if(arrayCPU[i].isOnUserMode())
					{
						Process prevProcess = arrayCPU[i].getExeProcess();
						readyQueues[prevProcess.getPriority() - 1].Enqueue(prevProcess);
						prevProcess.state = ProcessState.READY;

						arrayCPU[i].allocateProcess(p, p.getServiceTime(), false);
						p.state = ProcessState.EXECUTING;

						noCPUfound = false;
						i++;
					}
				}

				if(noCPUfound)
				{
					ftr.Enqueue(p);
					p.state = ProcessState.READY;
				}
			}
			else
			{
				ftrSuspended.Enqueue(p);
				p.state = ProcessState.SUSPENDED;
			}

		}

		//Schedule para user process que não se encontra na MP
		public void schNewUserProcess(Process p)
		{
			if(mp.allocate(p))
			{
				readyQueues[p.getPriority() - 1].Enqueue(p);
				p.state = ProcessState.READY;
			}
			else
			{
				suspended.Enqueue(p);
				p.state = ProcessState.SUSPENDED;
			}
		}

		public void schedule(CPU cpu, Process process, bool userMode)
		{
			if(process.getServiceTime() == 0)
			{
				if(userMode)
				{
					//unlock resources
					resMngr.unlockResources(process.getId());
				}
				//Deallocate memory
				mp.deallocate(process);
				//change state to finished
				process.state = ProcessState.FINISHED;	
			}
			else
			{
				int newPriority = process.getPriority() + 1;
				if (newPriority > 3)
					newPriority = 1;

				process.setPriority(newPriority);
				process.state = ProcessState.READY;
			}

			schedule(cpu);
		}

		public void schedule(CPU cpu)
		{
			if(ftr.Count > 0)
			{
				Process p = ftr.Dequeue();
				cpu.allocateProcess(p, p.getServiceTime(), false);
				p.state = ProcessState.EXECUTING;
			}
			else
			{
				for(int i = 0; i < readyQueues.Length; i++)
				{
					if (readyQueues[i].Count > 0)
					{
						Process p = readyQueues[i].Dequeue();
						cpu.allocateProcess(p, 2*(i+1), true);
						p.state = ProcessState.EXECUTING;
						break;
					}
				}	
			}
		}
	}
}