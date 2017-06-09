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
      int arrivalTime,
      int priority,
      int serviceTime,
      int mBytes,
      int printers,
      int scanners,
      int modems,
      int cdDrives
      )
    {
      this.setId(id);
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
      this.state = ProcessState.NEW;
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
            {
                terminate("PriorityOutOfBoundsExceptions(priority)");             
            }

      this.priority = priority;
    }

    private void setArrivalTime(int arrivalTime)
    {
      if (arrivalTime < 0)
            terminate("NegativeTimeException()");

      this.arrivalTime = arrivalTime;
    }

    public void setServiceTime(int serviceTime)
    {
      if (serviceTime < 0)
        terminate("NegativeTimeException()");

      this.serviceTime = serviceTime;
    }

    private void setMBytes(int mBytes)
    {
      if (mBytes < 0)
        terminate("NegativeMemoryAllocationException()");
      if (mBytes > Program.MEM_SIZE ||
          this.priority == 0 && mBytes > 64)
        terminate("MemoryAllocationOutOfBoundsException()");

      this.mBytes = mBytes;
    }

    private void setPrinters(int printers)
    {
      if (printers < 0)
        terminate("NegativeResourceSolicitationException()");
      if (printers > Program.PRINTERS)
        terminate("ResourceSolicitationOutOfBoundsException(" + printers + ")");

      this.printers = printers;
    }

    private void setScanners(int scanners)
    {
      if (scanners < 0)
        terminate("NegativeResourceSolicitationException()");
      if (scanners > Program.SCANNERS)
        terminate("ResourceSolicitationOutOfBoundsException(" + scanners + ")");

      this.scanners = scanners;
    }

    private void setModems(int modems)
    {
      if (modems < 0)
        terminate("NegativeResourceSolicitationException()");
      if (modems > Program.MODEMS)
        terminate("ResourceSolicitationOutOfBoundsException(" + modems + ")");

      this.modems = modems;
    }

    private void setCDDrives(int cdDrives)
    {
      if (cdDrives < 0)
        terminate("NegativeResourceSolicitationException()");
      if (cdDrives > Program.CD_DRIVES)
        terminate("ResourceSolicitationOutOfBoundsException(" + cdDrives +")");

      this.cdDrives = cdDrives;
    }

    private void setSize(int mBytes)
    {
       this.size = (int)Math.Ceiling((double)(mBytes/32));
    }

    public void updateResCount()
    {
      this.resourceCount--;
    }

        private void terminate(string msg)
        {
            Console.WriteLine(msg);
            System.Environment.Exit(1);
        }
  }
}
