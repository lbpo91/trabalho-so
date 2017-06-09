using System;
using System.Collections.Generic;

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
    private Queue<Process> waitingPrinter;
    private Queue<Process> waitingScanner;
    private Queue<Process> waitingModem;
    private Queue<Process> waitingCdDrive;

    public ResourceManager()
    {
      //Inicializa recursos
      printers = new Resource[Program.PRINTERS];
      for (int i = 0; i < printers.Length; i++)
        printers[i] = new Resource();

      scanners = new Resource[Program.SCANNERS];
      for (int i = 0; i < scanners.Length; i++)
        scanners[i] = new Resource();

      modems = new Resource[Program.MODEMS];
      for (int i = 0; i < modems.Length; i++)
        modems[i] = new Resource();

      cdDrives = new Resource[Program.CD_DRIVES];
      for (int i = 0; i < cdDrives.Length; i++)
        cdDrives[i] = new Resource();

      // Inicializa filas para recursos
      waitingPrinter = new Queue<Process>();
      waitingScanner = new Queue<Process>();
      waitingModem = new Queue<Process>();
      waitingCdDrive = new Queue<Process>();
    }
        public void display()
        {
            for (int i = 0; i < printers.Length; i++)
            {
                Console.WriteLine("Printer {0} info:", i + 1);
                printers[i].display();
                Console.WriteLine();
            }

            for (int i = 0; i < scanners.Length; i++)
            {
                Console.WriteLine("Scanner {0} info:", i + 1);
                scanners[i].display();
                Console.WriteLine();
            }

            for (int i = 0; i < modems.Length; i++)
            {
                Console.WriteLine("Modem {0} info:", i + 1);
                modems[i].display();
                Console.WriteLine();
            }

            for (int i = 0; i < cdDrives.Length; i++)
            {
                Console.WriteLine("CD drive {0} info:", i + 1);
                cdDrives[i].display();
                Console.WriteLine();
            }
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
      Queue<Process> waiting)
    {
      int i = 0;
      while (waiting.Count > 0 && i < res.Length)
      {
        if (!res[i].isLocked())
        {
          Process p = waiting.Dequeue();
          res[i].lockRes(p.getId());
          p.updateResCount();
        }
        i++;
      }
    }

    // Metodo libera todos os recursos de um processo
    public void unlockResources(int processId)
    {
      for (int i = 0; i < printers.Length; i++)
        printers[i].unlockRes(processId);

      for (int i = 0; i < scanners.Length; i++)
        scanners[i].unlockRes(processId);

      for (int i = 0; i < modems.Length; i++)
        modems[i].unlockRes(processId);

      for (int i = 0; i < cdDrives.Length; i++)
        cdDrives[i].unlockRes(processId);
    }

    //Coloca processo na fila de recursos
    public void requestResources(Process process)
    {
      for (int i = 0; i < process.getPrinters(); i++)
        waitingPrinter.Enqueue(process);

      for (int i = 0; i < process.getScanners(); i++)
        waitingScanner.Enqueue(process);

      for (int i = 0; i < process.getModems(); i++)
        waitingModem.Enqueue(process);

      for (int i = 0; i < process.getCDDrives(); i++)
        waitingCdDrive.Enqueue(process);
        
    }
  }
}