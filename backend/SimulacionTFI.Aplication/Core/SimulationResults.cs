using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionTFI.Aplication.Core
{
    // El reporte final que recibirá el Frontend
    public class SimulationResults
    {
        public int TotalDevicesArrived { get; set; }
        public int LaptopsArrived { get; set; }
        public int DesktopsArrived { get; set; }
        public int TotalDevicesProcessed { get; set; }
        public int RefurbishedCount { get; set; }
        public int RecycledCount { get; set; }
        public List<StageStats> Stages { get; set; } = new List<StageStats>();
        public double AluminioRecuperadoKg { get; set; }
        public double CobreRecuperadoKg { get; set; }
        public double HierroRecuperadoKg { get; set; }
        public double PlasticoAltaCalidad { get; set; }
        public double PlasticoMediaCalidad { get; set; }
        public double PlasticoBajaCalidad { get; set; }

        public int CantidadPlacasRecuperadas { get; set; }
    }
}
