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
			this.resMngr = rm;
			this.mp = mem;
		}

		//
		public void run()
		{
			if(ftr.Count == 0)
			{
				disabled.ForEach(delegate(Process p){
					//ResourceCount será 0 qnd o processo 
					//tiver todos os seus recursos reservados
					if(p.getResourceCount() == 0)
					{
						disabled.Remove(p);

						if(mp.hasSpace(p.getSize()))//tem memória
						{
							mp.allocate(p.getId(), p.getSize());
							readyQueues[p.getPriority() - 1].Enqueue(p);
							p.state = ProcessState.READY;
						}
						else //não tem memória
						{
							suspended.Enqueue(p);
							p.state = ProcessState.SUSPENDED;
						}
					}
				});
				if (suspended.Count > 0 && mp.hasSpace((suspended.Peek()).getSize()))
				{
					Process p = suspended.Dequeue();
					mp.allocate(p.getId(), p.getSize());
					readyQueues[p.getPriority() - 1].Enqueue(p);
					p.state = ProcessState.READY;
				}
			}
			
		}

		public void schedule(CPU cpu, Process process)
		{
			if(process.getServiceTime() == 0)
			{
				if(process.getPriority() != 0)
				{
					//unlock resources
					resMngr.unlockResources(process.getId());
				}
				//Deallocate memory
				mp.deallocate(process.getId());
				//change state to finished
				process.state = ProcessState.FINISHED;	
			}
			schedule(cpu);
		}

		public void schedule(CPU cpu)
		{
			if(ftr.Count > 0)
			{
				Process p = ftr.Dequeue();
				cpu.allocateProcess(p, p.getServiceTime());
				p.state = ProcessState.EXECUTING;
			}
			else
			{
				for(int i = 0; i < readyQueues.Length; i++)
				{
					if (readyQueues[i].Count > 0)
					{
						Process p = readyQueues[i].Dequeue();
						cpu.allocateProcess(p, 2*(i+1));
						p.state = ProcessState.EXECUTING;
						break;
					}
				}	
			}
		}
	}
}