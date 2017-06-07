using System.Collections;

namespace ProcessScheduler
{
  class Resource
  {
    private bool locked = false;

    private int processId;

    public void lock(int ownerId)
    {
      if (!this.locked)
      {
        this.processId = requestId;
        this.locked = true;
      }
    }

    public void unlock(int ownerId)
    {
      if(this.processId == requestId)
        this.locked = false;
    }

    public bool isLocked()
    {
      return locked;
    }
  }
}