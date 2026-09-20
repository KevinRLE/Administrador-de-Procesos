using System;
using System.Diagnostics;

namespace AdministradorProcesos.Services 
{
    public class MetricsService
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private double totalRamMB;

        public MetricsService()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            totalRamMB = gcMemoryInfo.TotalAvailableMemoryBytes / (1024.0 * 1024.0);

            cpuCounter.NextValue();
        }

        public float GetCpuUsage()
        {
            return cpuCounter.NextValue();
        }

        public double GetTotalRam()
        {
            return totalRamMB;
        }

        public float GetAvailableRam()
        {
            return ramCounter.NextValue();
        }

        public float GetUsedRamPercentage()
        {
            float availableRam = GetAvailableRam();
            double usedRam = totalRamMB - availableRam;
            return (float)((usedRam / totalRamMB) * 100);
        }
    }
}