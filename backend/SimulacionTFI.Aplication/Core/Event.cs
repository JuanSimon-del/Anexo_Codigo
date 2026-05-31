using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionTFI.Aplication.Core
{
    public enum EventType
    {
        Llegada,        // Un equipo llega a una etapa
        FinDeServicio,   // Un trabajador termina de procesar un equipo
        FinDeDia
    }

    public class Event
    {
        // El momento exacto en DÍAS en que ocurre este evento (Ej: 1.5, 3.2)
        public double EventTime { get; set; }

        public EventType Type { get; set; }

        // Identificador de la etapa (Ej: 1 = Inspección, 2 = Desarme, etc.)
        public int StageId { get; set; }

        public Event(double eventTime, EventType type, int stageId)
        {
            EventTime = eventTime;
            Type = type;
            StageId = stageId;
        }
    }
}