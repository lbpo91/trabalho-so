using System;

namespace ProcessScheduler
{
  public enum ProcessState
  {
    NEW,
    READY,
    EXECUTING,
    DISABLED,
    SUSPENDED,
    FINISHED
  }

  class Process
  {
    // Process identifier
    private int id;
    // Process priority (0 is highest priority)
    private int priority;
    // Time of arrival in CPU
    private int arrivalTime;
    // Time to complete execution
    private int serviceTime;
    // Size of memory allocation
    private int mBytes;
    // Amount of printers requested
    private int printers;
    // Amount of scanners requested
    private int scanners;
    // Amount of modems requested
    private int modems;
    // Amount of CD drives requested
    private int cdDrives;

    //Variaveis de controle interno(fora do enunciado)
    private int resourceCount;
    private int size;

    public ProcessState state;

    public Process (
      int id,
      int priority,
      int arrivalTime,
      int serviceTime,
      int mBytes,
      int printers,
      int scanners,
      int modems,
      int cdDrives
      )
    {
      this.setId();
      this.setPriority(priority);
      this.setArrivalTime(arrivalTime);
      this.setMBytes(mBytes);
      this.setPrinters(printers);
      this.setScanners(scanners);
      this.setModems(modems);
      this.setCDDrives(cdDrives);
      this.setServiceTime(serviceTime);
      this.resourceCount = printers + scanners 
        + modems + cdDrives;
      this.setSize(mBytes);
      this.ProcessState state = ProcessState.NEW;
    }

    /* ------GETTERS------ */

    public int getId()
    {
      return this.id;
    }

    public int getPriority()
    {
      return this.priority;
    }

    public int getArrivalTime()
    {
      return this.arrivalTime;
    }

    public int getServiceTime()
    {
      return this.serviceTime;
    }

    public int getMBytes()
    {
      return this.mBytes;
    }

    public int getPrinters()
    {
      return this.printers;
    }

    public int getScanners()
    {
      return this.scanners;
    }

    public int getModems()
    {
      return this.modems;
    }

    public int getCDDrives()
    {
      return this.cdDrives;
    }

    public ProcessState getState()
    {
      return this.state;
    }

    public int getResourceCount()
    {
      return this.resourceCount;
    }

    public int getSize()
    {
      return this.size;
    }

    /* ------SETTERS------ */

    private void setId(int id)
    {
      this.id = id;
    }

    public void setPriority(int priority)
    {
      if (priority < 0 || priority > 3)
      throw new PriorityOutOfBoundsExceptions(priority);

      this.priority = priority;
    }

    private void setArrivalTime(int arrivalTime)
    {
      if (arrivalTime < 0)
      throw new NegativeTimeException();

      this.arrivalTime = arrivalTime;
    }

    private void setServiceTime(int serviceTime)
    {
      if (serviceTime < 0)
      throw new NegativeTimeException();

      this.serviceTime = serviceTime;
    }

    private void setMBytes(int mBytes)
    {
      if (mBytes < 0)
      throw new NegativeMemoryAllocationException();
      if (mBytes > MEM_SIZE ||
          this.priority == 0 && mBytes > 64)
      throw new MemoryAllocationOutOfBoundsException();

      this.mBytes = mBytes;
    }

    private void setPrinters(int printers)
    {
      if (printers < 0)
      throw new NegativeResourceSolicitationException();
      if (printers > PRINTERS)
      throw new ResourceSolicitationOutOfBoundsException(
        printers
        );

      this.printers = printers;
    }

    private void setScanners(int scanners)
    {
      if (scanners < 0)
      throw new NegativeResourceSolicitationException();
      if (scanners > SCANNERS)
      throw new ResourceSolicitationOutOfBoundsException(
        scanners
        );

      this.scanners = scanners;
    }

    private void setModems(int modems)
    {
      if (modems < 0)
      throw new NegativeResourceSolicitationException();
      if (modems > MODEMS)
      throw new ResourceSolicitationOutOfBoundsException(
        modems
        );

      this.modems = modems;
    }

    private void setCDDrives(int cdDrives)
    {
      if (cdDrives < 0)
      throw new NegativeResourceSolicitationException();
      if (cdDrives > CD_DRIVES)
      throw new ResourceSolicitationOutOfBoundsException(
        cdDrives
        );

      this.cdDrives = cdDrives;
    }

    private void setSize(int mBytes)
    {
       this.size = (int)Math.Ceiling(mBytes / 32);

    }

    public void updateResCount()
    {
      this.resourceCount--;
    }
  }
}
