using System;
using System.Diagnostics;
using System.IO;

namespace AdministradorProcesos.Services
{
    //Funcion de operaciones sobre procesos
    public class ProcessOperationsService
    {
        //Funcion de cambio de prioridad
        public bool CambiarPrioridad(int processID, ProcessPriorityClass prioridad)
        {
            try
            {
                using (Process proceso = Process.GetProcessById(processID))
                {
                    proceso.PriorityClass = prioridad;
                    return true;
                }
            }
            catch { return false; }
        }
        
        //Ubicacion del archivo
        public string ObtenerUbicacionArchivo(int processID)
        {
            try
            {
                using (Process proceso = Process.GetProcessById(processID))
                {
                    return proceso.MainModule?.FileName ?? string.Empty;
                }
            }
            catch { return string.Empty; }
        }

        //Abrir la ubicacion del archivo 
        public bool AbrirUbicacionArchivo(int processID)
        {
            try
            {
                string ruta = ObtenerUbicacionArchivo(processID);
                if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta)){ return false; }

                string? carpeta=Path.GetDirectoryName(ruta);

                if (string.IsNullOrEmpty(carpeta)) { return false; }

                Process.Start(new ProcessStartInfo
                {
                    FileName="explorer.exe",
                    Arguments = $"/select,\"{ruta}\"",
                    UseShellExecute = true
                });
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}