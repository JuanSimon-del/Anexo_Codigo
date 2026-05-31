using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionTFI.Domain.Entities
{
    public enum MaterialType
    {
        Plastic,
        Aluminum,
        Copper, 
        Iron
    }
    public class Material
    {
        public Material(double weightMat, MaterialType typeMat)
        {
            this.weightMat = weightMat;
            this.typeMat = typeMat;
        }

        public double weightMat { get; set; }
        public MaterialType typeMat { get; set; }
    }
}
