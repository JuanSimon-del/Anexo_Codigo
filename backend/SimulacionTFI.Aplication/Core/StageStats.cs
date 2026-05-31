using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimulacionTFI.Aplication.Core
{
    public class StageStats
    {
        public string StageName { get; set; }
        public int ProcessedCount { get; set; }
        public int MaxQueueSize { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double KgEnCola { get; set; }
    }
}
