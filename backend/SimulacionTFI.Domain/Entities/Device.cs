using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionTFI.Domain.Entities
{
    public enum DeviceType
    {
        Laptop,
        Desktop
    }

    public class Device
    {
        public Device(double weight, DeviceType type)
        {
            this.weightDev = weight;
            this.typeDev = type;
        }

        public double weightDev { get; set; }
        public DeviceType typeDev { get; set; }

        // Sugerencia: Agrega este método a tu clase Device para calcular el despiece
        public List<Material> Desarmar()
        {
            var materiales = new List<Material>();

            if (this.typeDev == DeviceType.Laptop)
            {
                // Laptop: 50% metales, 25% plásticos, 30% componentes reutilizables 
                // (Nota: La suma es 105%, el modelo asume que el resto es lo no recuperable)
                materiales.Add(new Material(weightDev * 0.50, MaterialType.Aluminum));
                materiales.Add(new Material(weightDev * 0.25, MaterialType.Plastic));
                materiales.Add(new Material(weightDev * 0.30, MaterialType.Copper)); // Representando componentes
            }
            else // Es Desktop
            {
                // Desktop: 50% metales, 25% plásticos, 12% componentes reutilizables
                materiales.Add(new Material(weightDev * 0.50, MaterialType.Aluminum));
                materiales.Add(new Material(weightDev * 0.25, MaterialType.Plastic));
                materiales.Add(new Material(weightDev * 0.12, MaterialType.Copper));
            }

            double metalesTotal = weightDev * 0.50;

            materiales.Add(new Material(metalesTotal * 0.27, MaterialType.Iron));     // ¡AGREGADO!
            materiales.Add(new Material(metalesTotal * 0.20, MaterialType.Aluminum)); // Ajusta el resto proporcionalmente
            materiales.Add(new Material(metalesTotal * 0.03, MaterialType.Copper));   // Ajusta el resto proporcionalmente
            materiales.Add(new Material(weightDev * 0.25, MaterialType.Plastic));

            return materiales;
        }

    }
}
