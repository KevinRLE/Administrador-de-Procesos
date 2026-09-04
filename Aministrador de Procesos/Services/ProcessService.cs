using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AdministradorProcesos.Models;

namespace AdministradorProcesos.Services
{
    public class ProcessService
    {
        // Obtiene la lista actual de procesos
        public List<ProcessModel> GetActiveProcesses()
        {
            var processList = new List<ProcessModel>();
            Process[] processes = Process.GetProcesses();

            foreach (var p in processes)
            {
                try
                {
                    // Convertir la memoria de Bytes a Megabytes (MB)
                    long memoryMb = p.WorkingSet64 / (1024 * 1024);

                    processList.Add(new ProcessModel
                    {
                        Id = p.Id,
                        Name = p.ProcessName,
                        MemoryUsage = $"{memoryMb} MB",
                        Priority = p.BasePriority.ToString(),
                        Status = p.Responding ? "Activo" : "No responde"
                    });
                }
                catch
                {
                    // Algunos procesos del sistema restringen el acceso a sus propiedades
                    continue;
                }
            }

            return processList;
        }

        // Método para finalizar un proceso por su ID
        public bool KillProcess(int processId)
        {
            try
            {
                Process p = Process.GetProcessById(processId);
                p.Kill();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}