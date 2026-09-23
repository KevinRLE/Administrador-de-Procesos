//Creado por Britany Mishel Hernandez Davila 9959-24-4178
//Servicio para darle funcionalidad al apartado de prioridades y ubicacion del archivo del proceso
//Mandado a llamar por form1, para las acciones que requieren directamente permisos o manipulacion del SO
using System;
using System.Diagnostics; //Para poder manejar el explorador de windows y excepciones
using System.IO;

namespace AdministradorProcesos.Services
{
    //Funcion de operaciones sobre procesos
    public class ProcessOperationsService
    {
        //Funcion de cambio de prioridad del proceso
        //Recibe el PID y la prioridad que se desea cambiar
        public bool CambiarPrioridad(int processID, ProcessPriorityClass prioridad)
        {
            try
            {
                using (Process proceso = Process.GetProcessById(processID)) //Obtencion del proceso
                {
                    proceso.PriorityClass = prioridad; //Asignacion de la prioridad del proceso
                    return true;
                }
            }
            catch { return false; }
        }
        
        //Ubicacion de la ruta del ejecutable del proceso
        public string ObtenerUbicacionArchivo(int processID)
        {
            try
            {
                using (Process proceso = Process.GetProcessById(processID))
                {
                    return proceso.MainModule?.FileName ?? string.Empty; //Devuelve la ruta del proceso
                }
            }
            catch { return string.Empty; }
        }

        //Abrir el explorardor de archivos del archivo seleccionado
        public bool AbrirUbicacionArchivo(int processID)
        {
            try
            {
                //Usa a ObtenerUbicacionArchivo para obtener la ruta del proceso
                string ruta = ObtenerUbicacionArchivo(processID);
                //Verificacion de la existencia del proceso
                if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta)){ return false; } 

                string? carpeta=Path.GetDirectoryName(ruta); //Extraccion de la carpeta que contiene el proceso

                if (string.IsNullOrEmpty(carpeta)) { return false; } //Validacion de la carpeta

                //Apertura del explorador de archivos
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