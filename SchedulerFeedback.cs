namespace ProcessScheduer
{
	class SchedulerFeedback
	{
		private Queue<Process>[] readyQueues;
		private List<Process> blocked;
		private Queue<Process> suspended;

		public SchedulerFeedback()
		{
			this.readyQueues = new Queue<Process>[3];
			foreach(Queue<Process> q in readyQueues)
			{
				q = new Queue<Process>();
			}

			this.blocked = new List<Process>();
			this.suspended = new Queue<Process>();
		}

		//
		public void run()
		{
			blocked.ForEach(delegate(Process p){
					if(p.getResourceCount() == 0)
					{
						blocked.Remove(p);

						if(/*tem memoria aloca*/)
						{
							//aloca na memoria
							readyQueues[p.getPriority() - 1].Enqueue(p);
						}
						else //não tem memoria
						{
							suspended.Enqueue(p);
						}
					}
				});
			//if (checkMemorySpace(suspended.Peek().
			//getMBytes())){aloca memoria & readyQueues[p.getPriority() - 1].Enqueue(suspend.Dequeue());}
		}

		public void schedule(CPU cpu, Process process)
		{
			//unlock resources
			//change state to finished
			schedule(cpu);
		}

		public void schedule(CPU cpu)
		{
			for(int i = 0; i < readyQueues.Length; i++)
			{
				if (readyQueues[i].Count > 0)
				{
					cpu.allocateProcess(readyQueues[i].
						Dequeue(), Math.Pow(2, (i+1)), true);
					break;
				}
			}
		}
	}
}