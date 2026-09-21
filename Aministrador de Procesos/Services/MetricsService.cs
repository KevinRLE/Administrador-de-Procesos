using System;
using System.Diagnostics;

namespace AdministradorProcesos.Services 
{
    public class MetricsService
    {
        // Contadores nativos de Windows para leer hardware
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
       
        // Variable para guardar el total de memoria física instalada
        private double totalRamMB;

        public MetricsService()
        {
            // Inicializa el contador del procesador (Lee el tiempo total de uso de todos los núcleos)
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            // Inicializa el contador de RAM (Lee cuántos Megabytes están libres/disponibles)
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            // GC.GetGCMemoryInfo lee la memoria de la placa base.
            // Se divide entre 1024 dos veces para convertir Bytes a Megabytes.
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            totalRamMB = gcMemoryInfo.TotalAvailableMemoryBytes / (1024.0 * 1024.0);


            // Primera lectura obligatoria (el CPU siempre da 0 en la primera lectura, así que la gastamos aquí)
            cpuCounter.NextValue();
        }

        public float GetCpuUsage()
        {
            // Devuelve el porcentaje de uso actual del procesador
            return cpuCounter.NextValue();
        }

        public double GetTotalRam()
        {
            // Devuelve la RAM total instalada
            return totalRamMB;
        }

        public float GetAvailableRam()
        {
            // Devuelve cuánta RAM libre queda en este instante
            return ramCounter.NextValue();
        }

        public float GetUsedRamPercentage()
        {
            // Calcula el porcentaje: (RAM Usada / RAM Total) * 100
            float availableRam = GetAvailableRam();
            double usedRam = totalRamMB - availableRam;
            return (float)((usedRam / totalRamMB) * 100);
        }
    }
}