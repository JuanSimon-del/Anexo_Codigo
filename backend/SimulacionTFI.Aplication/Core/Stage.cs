using System;
using System.Collections.Generic;

namespace SimulacionTFI.Domain.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkersCount { get; set; }
        public int MaxQueueSize { get; private set; } = 0;

        private int _busyWorkers = 0;

        // Colas separadas para dispositivos y materiales
        private Queue<Device> _deviceQueue = new Queue<Device>();
        private Queue<Material> _materialQueue = new Queue<Material>();
        private List<Device> _devicesInService = new List<Device>();
        private List<Material> _materialsInService = new List<Material>();

        public Stage(int id, string name, int workersCount)
        {
            Id = id;
            Name = name;
            WorkersCount = workersCount;
        }

        // --- MÉTODOS PARA DISPOSITIVOS ---
        // 2. Modifica tu método AddToQueue
        public void AddToQueue(Device device)
        {
            _deviceQueue.Enqueue(device);

            // Si la cola actual es más grande que nuestro récord anterior, actualizamos el récord
            if (_deviceQueue.Count > MaxQueueSize)
            {
                MaxQueueSize = _deviceQueue.Count;
            }
        }
        public bool HasQueue() => _deviceQueue.Count > 0;
        public Device TakeFromQueue()
        {
            // Verificación de seguridad: si está vacía, no hagas Dequeue
            if (_deviceQueue.Count > 0)
                return _deviceQueue.Dequeue();
            return null; // Retorna null si no hay nada
        }
        public int GetQueueSize() => _deviceQueue.Count;

        public bool IsAvailable() => _devicesInService.Count < WorkersCount;

        public void StartService(Device device)
        {
            _busyWorkers++;
            _devicesInService.Add(device);
        }

        public Device EndService()
        {
            if (_devicesInService.Count > 0)
            {
                var dev = _devicesInService[0];
                _devicesInService.RemoveAt(0);
                return dev;
            }
            return null;
        }

        // --- MÉTODOS PARA MATERIALES (Lo que te faltaba) ---
        public void AddMaterialToQueue(Material material)
        {
            _materialQueue.Enqueue(material);

            // Si la cola actual es más grande que nuestro récord anterior, actualizamos el récord
            if (_materialQueue.Count > MaxQueueSize)
            {
                MaxQueueSize = _materialQueue.Count;
            }
        }

        public bool HasMaterialQueue() => _materialQueue.Count > 0;
        public Material TakeMaterialFromQueue()
        {
            // Verificación de seguridad: si está vacía, no hagas Dequeue
            if (_materialQueue.Count > 0)
                return _materialQueue.Dequeue();
            return null; // Retorna null si no hay nada
        }

        // 1. Inicia el servicio recibiendo el material específico
        public void StartServiceMaterial(Material material)
        {
            _busyWorkers++; // Incrementamos el contador de ocupados
            _materialsInService.Add(material); // Guardamos el material que estamos procesando
        }

        // Este método permite sacar un material de la cola para procesarlo en la Etapa 3
        public Material EndServiceMaterial()
        {
            if (_busyWorkers > 0) _busyWorkers--;

            if (_materialsInService.Count > 0)
            {
                var mat = _materialsInService[0];
                _materialsInService.RemoveAt(0);
                return mat;
            }
            return null;
        }
    }
}