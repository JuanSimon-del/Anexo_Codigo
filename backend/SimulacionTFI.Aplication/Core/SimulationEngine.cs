// SimulationEngine.cs es el corazón de la simulación. Este archivo define cómo avanzan los equipos y 
// materiales a través de las etapas, cómo se generan los tiempos y cómo se calculan los resultados.

using SimulacionTFI.Application.Extensions;
using SimulacionTFI.Domain.Entities;
using SimulacionTFI.Domain.Interfaces; // Solo conoces la interfaz del dominio
using System.Collections.Generic;
using System.Globalization;

namespace SimulacionTFI.Aplication.Core
{
    // Es una clase que simula una campaña de trabajo, maneja un calendario de eventos, modela 4 etapas de 
    // proceso, calcula resultados, usa un generador de números aleatorios

    public class SimulationEngine
    {
        private readonly IGenerator _generator; // solo dejamos este
        private EventCalendar _calendar; // guarda los eventos que deben ocurrir, ordenados por tiempo.
        private List<Stage> _stages;  // lista de etapas del proceso (Inspección, Desarme, Separación, Clasificación)
        private SimulationResults _results;  // guarda los resultados que se devolverán al final.

        public double CurrentTime { get; private set; } // es el tiempo actual de la simulación en días.
        public double TotalDuration { get; private set; } // es cuánto dura la campaña.

        // Este método se ejecuta cuando se crea un motor de simulación. Hace lo siguiente:
        // Guarda el generador aleatorio.
        // Crea el calendario de eventos.
        // Crea las 4 etapas.
        // Inicializa los resultados con estadísticas vacías para cada etapa.
        public SimulationEngine(int totalCampaigns, List<int> workersPerStage, IGenerator generator)
        {
            _generator = generator; // Asignamos la interfaz inyectada
            _calendar = new EventCalendar();
            _stages = new List<Stage>();

            TotalDuration = totalCampaigns * 7.0;
            CurrentTime = 0;

            _stages.Add(new Stage(1, "Inspección", workersPerStage[0]));
            _stages.Add(new Stage(2, "Desarme y Testeo", workersPerStage[1]));
            _stages.Add(new Stage(3, "Separación", workersPerStage[2]));
            _stages.Add(new Stage(4, "Clasificación", workersPerStage[3]));

            // Asegúrate de que los nombres aquí coincidan EXACTAMENTE con los que buscas luego
            _results = new SimulationResults();
            _results.Stages.Add(new StageStats { StageName = "Inspección", ProcessedCount = 0, MaxQueueSize = 0 });
            _results.Stages.Add(new StageStats { StageName = "Desarme y Testeo", ProcessedCount = 0, MaxQueueSize = 0 });
            _results.Stages.Add(new StageStats { StageName = "Separación", ProcessedCount = 0, MaxQueueSize = 0 });
            _results.Stages.Add(new StageStats { StageName = "Clasificación", ProcessedCount = 0, MaxQueueSize = 0 });
        }

        // Cambia el método Run para que reciba la duración de la campaña (7 días)
        // SimulationController.cs llama a engine.Run, por lo que duration=7
        public SimulationResults Run(double duration)
        {
            // Valores pre-establecidos para el calculo de Ingresos por Material
            double precioHierroKg = 170.0; 
            double precioAluminioKg = 250.0;
            double precioCobreKg = 1750.0;
            double precioPlastico = 20.0;
            double precioPlacas = 3000.0;

            TotalDuration = duration; // Definimos que esta ejecución dura solo una campaña
            CurrentTime = 0;

            // 1. FORZAMOS la primera llegada en el tiempo 0
            _calendar.AddEvent(new Event(0, EventType.Llegada, 1));

            // 2. AGENDAMOS el primer Fin de Día (en el tiempo 1.0, que equivale a 1 día de 8 horas)
            _calendar.AddEvent(new Event(1.0, EventType.FinDeDia, 4));

            // ¿hay eventos en el calendario?
            // CurrentTime < TotalDuration: ¿hemos llegado a los 7 días?
            while (_calendar.HasEvents() && CurrentTime < TotalDuration)
            {
                // Saca el evento más próximo del calendario.
                // Lo guarda en currentEvent.
                Event currentEvent = _calendar.GetNextEvent();
                System.Console.WriteLine($"Procesando evento {currentEvent.Type} en tiempo {currentEvent.EventTime}");
                CurrentTime = currentEvent.EventTime;

                switch (currentEvent.Type)
                {
                    case EventType.Llegada: ProcessArrival(currentEvent); break;
                    case EventType.FinDeServicio: ProcessDeparture(currentEvent); break;
                    // 3. AGREGAMOS EL CASO PARA EL FIN DE DÍA
                    case EventType.FinDeDia: ProcessFinDeDia(currentEvent); break;
                }
            }

            var stats1 = _results.Stages.FirstOrDefault(s => s.StageName == "Inspección");
            var stats2 = _results.Stages.FirstOrDefault(s => s.StageName == "Desarme y Testeo");
            var stats3 = _results.Stages.FirstOrDefault(s => s.StageName == "Separación");
            var stats4 = _results.Stages.FirstOrDefault(s => s.StageName == "Clasificación");

            // ETAPA 1: Ingresados - Procesados en Etapa 1
            if (stats1 != null)
            {
                stats1.NotProcessedCount = _results.TotalDevicesArrived - stats1.ProcessedCount;
            }

            // ETAPA 2: Ingresados (De Etapa 1) - Procesados en Etapa 2
            if (stats2 != null && stats1 != null)
            {
                // Al total procesado de la Etapa 1 le restamos los reacondicionados (que no van a desarme)
                int ingresaronEtapa2 = stats1.ProcessedCount - _results.RefurbishedCount;
                stats2.NotProcessedCount = ingresaronEtapa2 - stats2.ProcessedCount;
            }

            // ETAPA 3: Cada dispositivo procesado en Etapa 2 genera materiales que entran aquí.
            // Si se procesaron Y dispositivos en la Etapa 2, entraron (Y * 7) componentes.
            // Para calcularlo en "unidades de dispositivo equivalente": (Componentes Ingresados / 7) - (Componentes Procesados / 7)
            if (stats3 != null && stats2 != null)
            {
                double ingresaronEquivalentesEtapa3 = stats2.ProcessedCount; // Ya que Y2 * 7 / 7 = Y2
                double procesadosEquivalentesEtapa3 = stats3.ProcessedCount / 7.0;

                // Guardamos la diferencia como un entero aproximado
                stats3.NotProcessedCount = (int)Math.Round(ingresaronEquivalentesEtapa3 - procesadosEquivalentesEtapa3);
            }

            // ETAPA 4: Funciona de forma diaria por lote (Kilos), por lo que se mide en kg acumulados en cola
            if (stats4 != null)
            {
                // Al final del último día, lo que no se procesó es lo que quedó físicamente en KgEnCola
                stats4.NotProcessedCount = (int)Math.Round(stats4.KgEnCola);
            }

            foreach (var stage in _stages)
            {
                var stats = _results.Stages.FirstOrDefault(s => s.StageName == stage.Name);
                if (stats != null)
                {
                    // Pasamos el récord de la etapa a los resultados del JSON
                    stats.MaxQueueSize = stage.MaxQueueSize;
                }
            }

            // Obtenemos cuántos se procesaron en la Etapa 1 (Inspección)
            var inspeccionEstadistica = _results.Stages.FirstOrDefault(s => s.StageName == "Inspección");

            // Calculamos los que se quedaron sin procesar
            _results.TotalDevicesNotProcessed = _results.TotalDevicesArrived - (stats1 != null ? stats1.ProcessedCount : 0);

            // Calculamos el Ingreso Generado por la venta de los materiales
            _results.IngresosMetales = _results.AluminioRecuperadoKg * precioAluminioKg + _results.HierroRecuperadoKg * precioHierroKg + _results.CobreRecuperadoKg * precioCobreKg;
            _results.IngresosPlacas = _results.CantidadPlacasRecuperadas * precioPlacas;
            _results.IngresosPlasticos = (_results.PlasticoAltaCalidad + _results.PlasticoBajaCalidad + _results.PlasticoMediaCalidad) * precioPlastico;

            // Calculamos el Ingreso Total Generado
            _results.TotalIngresosGenerados = _results.IngresosPlacas + _results.IngresosMetales + _results.IngresosPlasticos;

            return _results;
        }

        private Device CreateRandomDevice()
        {
            // 65% Laptop, 35% Desktop
            double prob = _generator.SiguienteUniforme();
            var type = (prob < 0.65) ? DeviceType.Laptop : DeviceType.Desktop;

            // Peso: Laptop (2.25 +- 0.75), Desktop (8 +- 2)
            double weight = (type == DeviceType.Laptop)
                ? 1.5 + (_generator.SiguienteUniforme() * 1.5) // Rango [1.5, 3]
                : 6.0 + (_generator.SiguienteUniforme() * 4.0); // Rango [6, 10]

            return new Device(weight, type);
        }

        private void ProcessDeparture(Event e)
        {
            var currentStage = _stages.Find(s => s.Id == e.StageId);
            if (currentStage == null) return;

            var stageStats = _results.Stages.FirstOrDefault(s => s.StageName == currentStage.Name);
            if (stageStats == null) return;

            // 1. OBTENER EL ELEMENTO QUE TERMINÓ Y LIBERAR AL TRABAJADOR
            Device finishedDevice = null;
            Material finishedMaterial = null;

            if (e.StageId == 3)
            {
                finishedMaterial = currentStage.EndServiceMaterial();
            }
            else
            {
                finishedDevice = currentStage.EndService();
            }

            // 2. ACTUALIZAR ESTADÍSTICAS GENERALES
            stageStats.ProcessedCount++;
            if (e.StageId == 1)
            {
                _results.TotalDevicesProcessed++;
            }

            // 3. RUTEO DEL ELEMENTO QUE ACABA DE TERMINAR
            if (e.StageId == 1 && finishedDevice != null) // Inspección
            {
                if (_generator.SiguienteUniforme() < 0.15)
                    _results.RefurbishedCount++;
                else
                    TransferEntity(e.StageId, 2, finishedDevice);
            }
            else if (e.StageId == 2 && finishedDevice != null) // Desarme
            {
                _results.RecycledCount++;
                _results.CantidadPlacasRecuperadas++;

                var materiales = finishedDevice.Desarmar();
                var stage3 = _stages.Find(s => s.Id == 3);

                // Agregamos todo a la cola de la Etapa 3
                foreach (var mat in materiales)
                {
                    stage3.AddMaterialToQueue(mat);
                }

                // IMPORTANTE: Despertamos a TODOS los trabajadores libres de la Etapa 3
                while (stage3.IsAvailable() && stage3.HasMaterialQueue())
                {
                    var nextMat = stage3.TakeMaterialFromQueue();
                    if (nextMat != null)
                    {
                        stage3.StartServiceMaterial(nextMat);
                        double capacidadPorHora = 15.0 + (_generator.SiguienteUniforme() * 15.0);
                        double horas = nextMat.weightMat / capacidadPorHora;
                        _calendar.AddEvent(new Event(CurrentTime + (horas / 8.0), EventType.FinDeServicio, 3));
                    }
                }
            }
            else if (e.StageId == 3 && finishedMaterial != null) // Separación
            {
                if (finishedMaterial.typeMat == MaterialType.Plastic)
                {
                    var stage4Stats = _results.Stages.FirstOrDefault(s => s.StageName == "Clasificación");
                    if (stage4Stats != null) stage4Stats.KgEnCola += finishedMaterial.weightMat;
                }
                else if (finishedMaterial.typeMat == MaterialType.Aluminum)
                    _results.AluminioRecuperadoKg += finishedMaterial.weightMat;
                else if (finishedMaterial.typeMat == MaterialType.Copper)
                    _results.CobreRecuperadoKg += finishedMaterial.weightMat;
                else if (finishedMaterial.typeMat == MaterialType.Iron) // ¡AGREGADO!
                    _results.HierroRecuperadoKg += finishedMaterial.weightMat;
            }

            // 4. INICIAR NUEVO SERVICIO EN LA ETAPA ACTUAL (Avanzar la cola)
            // Usamos 'while' para asegurar que si hay 3 trabajadores libres y 5 en cola, los 3 arranquen de inmediato.
            while (currentStage.IsAvailable())
            {
                if (e.StageId < 3 && currentStage.HasQueue())
                {
                    var nextDevice = currentStage.TakeFromQueue();
                    if (nextDevice != null)
                    {
                        currentStage.StartService(nextDevice);
                        double serviceTime = 0.1 + (_generator.SiguienteUniforme() * 0.2);
                        _calendar.AddEvent(new Event(CurrentTime + serviceTime, EventType.FinDeServicio, currentStage.Id));
                    }
                }
                else if (e.StageId == 3 && currentStage.HasMaterialQueue())
                {
                    var nextMat = currentStage.TakeMaterialFromQueue();
                    if (nextMat != null)
                    {
                        currentStage.StartServiceMaterial(nextMat);
                        double capacidadPorHora = 15.0 + (_generator.SiguienteUniforme() * 15.0);
                        double horas = nextMat.weightMat / capacidadPorHora;
                        _calendar.AddEvent(new Event(CurrentTime + (horas / 8.0), EventType.FinDeServicio, 3));
                    }
                }
                else
                {
                    break; // Si no hay cola o no hay trabajadores libres, salimos del bucle
                }
            }
        }

        private void ProcessPlasticClassification(int trabajadoresEtapa4)
        {
            // Capacidad diaria total de la etapa
            double capacidadDiaria = trabajadoresEtapa4 * 120.0;

            // Obtenemos los kilos acumulados en la etapa 3 (Separación) que pasaron a etapa 4
            var stage4Stats = _results.Stages.Find(s => s.StageName == "Clasificación");

            // Cantidad a procesar hoy (lo que haya en cola o la capacidad máxima)
            double aProcesar = Math.Min(stage4Stats.KgEnCola, capacidadDiaria);

            // Clasificación por calidades
            double alta = aProcesar * 0.20;
            double media = aProcesar * 0.50;
            double baja = aProcesar * 0.30;

            // Actualizamos los resultados
            stage4Stats.ProcessedCount += (int)aProcesar; // Si prefieres contar en Kg
            stage4Stats.KgEnCola -= aProcesar;

            // Aquí guardas los totales de plástico recuperado por calidad
            _results.PlasticoAltaCalidad += alta;
            _results.PlasticoMediaCalidad += media;
            _results.PlasticoBajaCalidad += baja;
        }

        private void ProcessFinDeDia(Event e)
        {
            // Ejecutamos la lógica de clasificación de plásticos
            int trabajadoresEtapa4 = _stages.Find(s => s.Id == 4).WorkersCount;
            ProcessPlasticClassification(trabajadoresEtapa4);

            // Agendamos el próximo fin de día (sumamos 1 día al tiempo actual)
            if (CurrentTime + 1.0 <= TotalDuration)
            {
                _calendar.AddEvent(new Event(CurrentTime + 1.0, EventType.FinDeDia, 4));
            }
        }

        private void ProcessArrival(Event e)
        {
            int cantidadLlegada = (int)Math.Round(_generator.NextNormal(90, 10));

            _results.TotalDevicesArrived += cantidadLlegada;

            var stage = _stages.Find(s => s.Id == e.StageId);

            for (int i = 0; i < cantidadLlegada; i++)
            {
                var device = CreateRandomDevice();

                if (device.typeDev == DeviceType.Laptop)
                    _results.LaptopsArrived++;
                else
                    _results.DesktopsArrived++;

                if (stage.IsAvailable())
                {
                    stage.StartService(device);
                    // Inspección: 2 Dispositivos/hora (0.5h)
                    double serviceTime = 0.5;
                    _calendar.AddEvent(new Event(CurrentTime + (serviceTime / 8.0), EventType.FinDeServicio, stage.Id));
                }
                else
                {
                    stage.AddToQueue(device);
                }
            }
        }

        private void ScheduleNextArrival()
        {
            // Usamos el generador para darle variabilidad a ese número 
            double timeToNextArrival = 7.0;
            _calendar.AddEvent(new Event(CurrentTime + timeToNextArrival, EventType.Llegada, 1));
        }

        
        private void TransferEntity(int currentStageId, int nextStageId, Device device)
        {
            // 1. Buscamos la etapa destino
            var nextStage = _stages.Find(s => s.Id == nextStageId);

            if (nextStage == null)
            {
                Console.WriteLine($"Error: No se encontró la etapa {nextStageId}");
                return;
            }

            // 2. Agregamos el dispositivo a la cola destino
            nextStage.AddToQueue(device);

            // 3. Verificamos disponibilidad y arrancamos el proceso si se puede
            if (nextStage.IsAvailable())
            {
                // Sacamos el dispositivo de la cola porque lo vamos a procesar YA
                var devToProcess = nextStage.TakeFromQueue();

                // Arrancamos el servicio
                nextStage.StartService(devToProcess);

                // Calculamos tiempo de servicio (ajusta según la etapa si es necesario)
                double serviceTime = 0.1 + (_generator.SiguienteUniforme() * 0.2);
                _calendar.AddEvent(new Event(CurrentTime + serviceTime, EventType.FinDeServicio, nextStage.Id));
            }
        }
    }
}