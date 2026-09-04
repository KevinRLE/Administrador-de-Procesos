using System;
using System.Collections.Generic;
using System.Text;

namespace AdministradorProcesos.Models
{
    public class ProcessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MemoryUsage { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
