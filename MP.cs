using System;
using System.Collections.Generic;

namespace ProcessScheduler
{
	class MP
	{
		private int[] blocks;
		private int emptyBlocks;
		private List<Process> userProcessesOnMP;

		public MP()
		{
			this.blocks = new int[32];
			this.emptyBlocks = 32;
			this.userProcessesOnMP = new List<Process>();
		}

		public bool hasSpace(int processSize)
		{

			if(emptyBlocks >= processSize)
				return true;
			else
				return false;
		}

		private Process[] findCandidates(int size)
		{
			Process candidate = null;
			for(int i = 0; i < userProcessesOnMP.Count; i++)
			{
				Process p = userProcessesOnMP[i];
				if(p.getSize() == size)
				{
					return new Process[] {p};
				}
				else if (p.getSize() > size)
				{
					if(candidate != null)
					{
						if(p.getSize() < candidate.getSize())
							candidate = p;
					}
					else
					{
						candidate = p;
					}
				}
			}

			if(candidate != null)
				return new Process[] {candidate};

			if(userProcessesOnMP.Count > 2)
			{
				Process[] candidateCouple = new Process[2];
				int sum = 0;
				
				for(int i = 0; i < userProcessesOnMP.Count - 1; i++)
				{
					for(int j = i + 1; j < userProcessesOnMP.Count; j++)
					{
						int sumAux = userProcessesOnMP[i].getSize() + userProcessesOnMP[j].getSize();
						if(sumAux >= size)
						{
							if(sum == 0 || sumAux < sum)
							{
								candidateCouple[0] = userProcessesOnMP[i];
								candidateCouple[1] = userProcessesOnMP[j];
								sum = candidateCouple[0].getSize() + candidateCouple[1].getSize();
							}
						}
					}
				}

				if(sum != 0)
					return candidateCouple;

			}
		
			return new Process[] {null};	
		}

		public bool createSpaceForFTR(int processSize)
		{	
			Process[] candidates = findCandidates(processSize);
		
			if(candidates[0] == null)
				return false;
			
			for(int i = 0; i < candidates.Length; i++)
			{
				deallocate(candidates[i]);
				candidates[i].state = ProcessState.SUSPENDED;
			}

			return true;
		}

		public bool allocate(Process p)
		{
			int processSize = p.getSize();
			int processId = p.getId();

			if(!hasSpace(processSize))
			{
				if(p.getPriority() == 0)
				{
					if(!createSpaceForFTR(processSize))
						return false;
				}
				else
				{
					return false;
				}
			}

			List<int> availables = new List<int>();
			for(int i = 0; i < 32; i++)
			{
				if(blocks[i] != 0)
				{
					availables.Add(i);
					if(availables.Count == processSize)
					{
						availables.ForEach(delegate(int e){
							blocks[e] = processId;
						});
						emptyBlocks -= processSize;
						break;
					}
				}
			}
			if(p.getPriority() != 0)
				userProcessesOnMP.Add(p);

			return true;
		}

		public void deallocate(Process p)
		{
			for(int i = 0; i < 32; i++)
			{
				if(blocks[i] == p.getId())
				{
					blocks[i] = 0;
					emptyBlocks++;
				}
			}
			if(p.getPriority() != 0)
				userProcessesOnMP.Remove(p);
		}
	}
}