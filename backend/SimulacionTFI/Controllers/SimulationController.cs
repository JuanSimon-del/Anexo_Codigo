using Microsoft.AspNetCore.Mvc;
using SimulacionTFI.Aplication.Core;
using SimulacionTFI.Aplication.Core.Dtos;
using SimulacionTFI.Aplication.Dtos;
using SimulacionTFI.Domain.Interfaces;

// Un controlador es la parte que recibe solicitudes desde el navegador o desde 
// otra aplicación y decide qué hacer.

namespace SimulacionTFI.API.Controllers
{
    [ApiController]

    // define la ruta base para este controlador.
    // [controller] se reemplaza por Simulation, por lo que la ruta base es api/simulation.
    [Route("api/[controller]")]

    public class SimulationController : ControllerBase
    {
        private readonly IGenerator _generator;

        // SimulationController recibe una petición para ejecutar la simulación y devuelve los resultados.
        // IGenerator es una interfaz que define cómo obtener números aleatorios.
        // _generator es una variable que guarda ese generador.
        // Esto significa: cuando llega una petición, el controlador ya tiene un generador de números
        // aleatorios listo para usar.

        public SimulationController(IGenerator generator)
        {
            _generator = generator;
        }

        [HttpPost("run")]

        // Crea un objeto response que contendrá:
        // CampaignDetails: resultados individuales de cada campaña.
        // Summary: un resumen acumulado de todas las campañas.
        public IActionResult RunSimulation([FromBody] SimulationRequest request)
        {
            var response = new SimulationResponseDto();
            response.CampaignDetails = new List<SimulationResults>();

            // Inicializamos el resumen global con ceros
            response.Summary = new SimulationResults();

            // Por cada campaña: Crea un nuevo SimulationEngine
            // recibe 1 campaña, la lista de trabajadores por etapa y el generador de números aleatorios.
            for (int i = 0; i < request.TotalCampaigns; i++)
            {
                // Creamos una nueva instancia del motor para cada campaña
                var engine = new SimulationEngine(1, request.WorkersPerStage, _generator);

                // Esto simula 7 días
                var result = engine.Run(7.0);

                // --- REDONDEO DE DECIMALES ---
                // Redondeamos a 2 decimales los resultados de esta campaña antes de guardarlos
                result.AluminioRecuperadoKg = Math.Round(result.AluminioRecuperadoKg, 2);
                result.CobreRecuperadoKg = Math.Round(result.CobreRecuperadoKg, 2);
                result.HierroRecuperadoKg = Math.Round(result.HierroRecuperadoKg, 2);
                result.PlasticoAltaCalidad = Math.Round(result.PlasticoAltaCalidad, 2);
                result.PlasticoMediaCalidad = Math.Round(result.PlasticoMediaCalidad, 2);
                result.PlasticoBajaCalidad = Math.Round(result.PlasticoBajaCalidad, 2);
                result.IngresosMetales = Math.Round(result.IngresosMetales, 2);
                result.IngresosPlacas = Math.Round(result.IngresosPlacas, 2);
                result.IngresosPlasticos = Math.Round(result.IngresosPlasticos, 2);
                result.TotalIngresosGenerados = Math.Round(result.TotalIngresosGenerados, 2);

                foreach (var stage in result.Stages)
                {
                    stage.KgEnCola = Math.Round(stage.KgEnCola, 2);
                }

                // Guardamos los resultados individuales en la lista
                response.CampaignDetails.Add(result);

                //Sumamos las cantidades de equipos al resumen global
                response.Summary.TotalDevicesArrived += result.TotalDevicesArrived;
                response.Summary.LaptopsArrived += result.LaptopsArrived;
                response.Summary.DesktopsArrived += result.DesktopsArrived;

                // Sumamos cantidades de equipos
                response.Summary.TotalDevicesProcessed += result.TotalDevicesProcessed;
                response.Summary.RefurbishedCount += result.RefurbishedCount;
                response.Summary.RecycledCount += result.RecycledCount;
                response.Summary.TotalDevicesNotProcessed += response.Summary.TotalDevicesNotProcessed;

                // Sumamos los kilos de materiales recuperados
                response.Summary.AluminioRecuperadoKg += result.AluminioRecuperadoKg;
                response.Summary.CobreRecuperadoKg += result.CobreRecuperadoKg;
                response.Summary.HierroRecuperadoKg += result.HierroRecuperadoKg;
                response.Summary.PlasticoAltaCalidad += result.PlasticoAltaCalidad;
                response.Summary.PlasticoMediaCalidad += result.PlasticoMediaCalidad;
                response.Summary.PlasticoBajaCalidad += result.PlasticoBajaCalidad;
                response.Summary.CantidadPlacasRecuperadas += result.CantidadPlacasRecuperadas;

                // Sumamos los ingresos de materiales
                response.Summary.IngresosMetales += result.IngresosMetales;
                response.Summary.IngresosPlacas += result.IngresosPlacas;
                response.Summary.IngresosPlasticos += result.IngresosPlasticos;
                response.Summary.TotalIngresosGenerados += result.TotalIngresosGenerados;
            }

            // Por seguridad: Redondeamos el resumen global al final 
            response.Summary.AluminioRecuperadoKg = Math.Round(response.Summary.AluminioRecuperadoKg, 2);
            response.Summary.CobreRecuperadoKg = Math.Round(response.Summary.CobreRecuperadoKg, 2);
            response.Summary.HierroRecuperadoKg = Math.Round(response.Summary.HierroRecuperadoKg, 2);
            response.Summary.PlasticoAltaCalidad = Math.Round(response.Summary.PlasticoAltaCalidad, 2);
            response.Summary.PlasticoMediaCalidad = Math.Round(response.Summary.PlasticoMediaCalidad, 2);
            response.Summary.PlasticoBajaCalidad = Math.Round(response.Summary.PlasticoBajaCalidad, 2);
            response.Summary.IngresosMetales = Math.Round(response.Summary.IngresosMetales, 2);
            response.Summary.IngresosPlacas = Math.Round(response.Summary.IngresosPlacas, 2);
            response.Summary.IngresosPlasticos = Math.Round(response.Summary.IngresosPlasticos, 2);
            response.Summary.TotalIngresosGenerados = Math.Round(response.Summary.TotalIngresosGenerados, 2);

            return Ok(response);
        }
    }
}
