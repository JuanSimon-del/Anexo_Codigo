using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimulacionTFI.Aplication.Dtos
{
    public class SimulationRequest
    {
        [Required(ErrorMessage = "La cantidad de campañas es obligatoria.")]
        [Range(1, 50, ErrorMessage = "La cantidad de campañas debe ser entre 1 y 50.")]
        public int TotalCampaigns { get; set; }

        [Required(ErrorMessage = "Debes especificar los trabajadores por etapa.")]
        // Validamos que la lista tenga exactamente 4 elementos (uno por cada etapa)
        [MinLength(4, ErrorMessage = "Se requieren datos para las 4 etapas.")]
        [MaxLength(4, ErrorMessage = "Se requieren datos para las 4 etapas.")]
        public List<int> WorkersPerStage { get; set; } = new List<int>();
    }
}