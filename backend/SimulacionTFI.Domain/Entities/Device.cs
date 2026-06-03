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

        public List<Material> Desarmar()
        {
            var materiales = new List<Material>();

            if (this.typeDev == DeviceType.Laptop)
            {
                // Laptop: 50% metales, 15% plásticos, 20% componentes reutilizables, 15% no recuperables
                materiales.Add(new Material(weightDev * 0.50, MaterialType.Metal));
                materiales.Add(new Material(weightDev * 0.15, MaterialType.Plastic));
                materiales.Add(new Material(weightDev * 0.20, MaterialType.Recovery));
                materiales.Add(new Material(weightDev * 0.15, MaterialType.Trash));
            }
            else // Es Desktop
            {
                // Desktop: 50% metales, 25% plásticos, 12% componentes reutilizables, 13% no recuperables
                materiales.Add(new Material(weightDev * 0.50, MaterialType.Metal));
                materiales.Add(new Material(weightDev * 0.25, MaterialType.Plastic));
                materiales.Add(new Material(weightDev * 0.12, MaterialType.Recovery));
                materiales.Add(new Material(weightDev * 0.13, MaterialType.Trash));
            }

            double metalesTotal = weightDev * 0.50;

            materiales.Add(new Material(metalesTotal * 0.27, MaterialType.Iron));
            materiales.Add(new Material(metalesTotal * 0.10, MaterialType.Aluminum));
            materiales.Add(new Material(metalesTotal * 0.08, MaterialType.Copper));

            return materiales;
        }

    }
}
