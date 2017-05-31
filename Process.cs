#define PRINTERS Main.PRINTERS
#define SCANNERS Main.SCANNERS
#define MODEMS Main.MODEMS
#define CD_DRIVES Main.CD_DRIVES
#define MEM_SIZE Main.MEM_SIZE

using System;

namespace ProcessScheduler
{
  class Process
  {
    // Number of processes created in runtime
    private static processNumber;

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

    private int resourceCount;

    private ProcessState state = ProcessState.NEW;

    public Process (
      priority,
      arrivalTime,
      serviceTime,
      mBytes,
      printers,
      scanners,
      modems,
      cdDrives
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

    /* ------SETTERS------ */

    private void setId()
    {
      this.id = processNumber++;
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

    private void updateResCount()
    {
      this.resourceCount--;
    }
  }
}

public enum ProcessState
{
  NEW,
  READY,
  EXECUTING,
  BLOCKED,
  READY-SUSPENDED,
  BLOCKED-SUSPENDED,
  FINISHED
}
