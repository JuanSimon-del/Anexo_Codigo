using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionTFI.Aplication.Core.Dtos
{
    public class SimulationResponseDto
    {
        public List<SimulationResults> CampaignDetails { get; set; } = new List<SimulationResults>();
        public SimulationResults Summary { get; set; }

        public double MetalesRecuperadosKg;

        public double PlásticosRecuperadosKg;
    }

    public class StageStatsDto
    {
        public string Name { get; set; }
        public int Processed { get; set; }
        public int MaxQueue { get; set; }
    }
}
