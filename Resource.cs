using System;

namespace ProcessScheduler
{
  class Resource
  {
    private bool locked = false;

    private int processId;

        public void display()
        {
            if (locked)
                Console.WriteLine("Being used by process with Id#: {0}", processId);
            else
                Console.WriteLine("Not being used.");
        }

    public void lockRes(int ownerId)
    {
      if (!this.locked)
      {
        this.processId = ownerId;
        this.locked = true;
      }
    }

    public void unlockRes(int ownerId)
    {
      if(this.processId == ownerId)
        this.locked = false;
    }

    public bool isLocked()
    {
      return locked;
    }
  }
}