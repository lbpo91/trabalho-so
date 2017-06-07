using System;
using System.Collections.Generic;

namespace ProcessScheduler
{
	class MP
	{
		private int[] blocks;
		private int emptyBlocks;

		public MP()
		{
			this.blocks = new int[32];
			this.emptyBlocks = 32;
		}

		public bool hasSpace(int processSize)
		{
			if(emptyBlocks >= processSize)
				return true;
			else
				return false;
		}

		public void allocate(int processId, int processSize)
		{
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
		}

		public void deallocate(int processId)
		{
			for(int i = 0; i < 32; i++)
			{
				if(blocks[i] == processId)
				{
					blocks[i] = 0;
					emptyBlocks++;
				}
			}
		}
	}
}