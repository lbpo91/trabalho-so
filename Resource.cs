using System.Collections;

namespace ProcessScheduler
{
  class Resource
  {
    private bool locked = false;

    private int processId;

    public int lock(int ownerId)
    {
      if (this.locked)
        return 0;
      else {
        this.processId = requestId;
        this.locked = true;
        return 1;
      }
    }

    public int unlock(int ownerId)
    {
      if(this.processId != requestId)
        return 0;
      else
        this.locked = false;
    }

    public bool isLocked()
    {
      return locked;
    }
  }
}