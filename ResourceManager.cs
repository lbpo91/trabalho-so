namespace ProcessScheduler
{
  class ResourceManager
  {
    //Coleção de recursos
    private Resource[] printers;
    private Resource[] scanners;
    private Resource[] modems;
    private Resource[] cdDrives;

    //Filas para cada recurso
    private Queue<int> waitingPrinter;
    private Queue<int> waitingScanner;
    private Queue<int> waitingModem;
    private Queue<int> waitingCdDrive;

    //Memoria
    private int[] memoryBlocks;

    public ResourceManager(
      int printerNum,
      int scannerNum,
      int modemNum,
      int cdDriveNum)
    {
      //Inicializa recursos
      printers = new Resource[Main.PRINTERS];
      for (int i = 0; i < printers.Length; i++)
        printers[i] = new Resource();

      scanners = new Resource[Main.SCANNERS];
      for (int i = 0; i < scanners.Length; i++)
        scanners[i] = new Resource();

      modems = new Resource[Main.MODEMS];
      for (int i = 0; i < modems.Length; i++)
        modems[i] = new Resource();

      cdDrives = new Resource[Main.CD_DRIVES];
      for (int i = 0; i < cdDrives.Length; i++)
        cdDrives[i] = new Resource();

      // Inicializa filas para recursos
      waitingPrinter = new Queue<int>();
      waitingScanner = new Queue<int>();
      waitingModem = new Queue<int>();
      waitingCdDrive = new Queue<int>();

      //Memoria
      memoryBlocks = new int[32];
    }

    // Metodo que roda todo ciclo checando recursos livres
    public void run()
    {
      //Verifica cada tipo de recurso
      checkQueue(printers, waitingPrinter);
      checkQueue(scanners, waitingScanner);
      checkQueue(modems, waitingModem);
      checkQueue(cdDrives, waitingCdDrive);
    }

    private void checkQueue(Resource[] res, 
      Queue<int> waiting)
    {
      int i = 0;
      while (waiting.Count > 0 && i < res.Length)
      {
        if (!res[i].isLocked())
        {
          int processId = waiting.Dequeue();
          res[i].lock(processId);
        }
        i++;
      }
    }

    // Metodo libera todos os recursos de um processo
    public void unlockResources(int processId)
    {
      for (int i = 0; i < printers.Length; i++)
        printers[i].unlock(processId);

      for (int i = 0; i < scanners.Length; i++)
        scanners[i] = unlock(processId);

      for (int i = 0; i < modems.Length; i++)
        modems[i] = unlock(processId);

      for (int i = 0; i < cdDrives.Length; i++)
        cdDrives[i] = unlock(processId);
    }

    //Coloca processo na fila de recursos
    public void requestResources(int processId, 
      int reqPrinters, int reqScanners, int reqModems, 
      int reqCdDrives)
    {
      for (int i = 0; i < reqPrinters; i++)
        waitingPrinter.Enqueue(processId);

      for (int i = 0; i < reqScanners; i++)
        waitingScanner.Enqueue(processId);

      for (int i = 0; i < reqModems; i++)
        waitingModem.Enqueue(processId);

      for (int i = 0; i < reqCdDrives; i++)
        waitingCdDrive.Enqueue(processId);
    }
  }
}