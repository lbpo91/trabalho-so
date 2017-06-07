
namespace ProcessScheduler
{
  class Resource
  {
    private bool locked = false;

    private int processId;

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