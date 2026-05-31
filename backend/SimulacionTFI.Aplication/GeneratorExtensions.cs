using System;
using SimulacionTFI.Domain.Interfaces;

namespace SimulacionTFI.Application.Extensions
{
    public static class GeneratorExtensions
    {
        public static double NextNormal(this IGenerator generator, double media, double desviacionEstandar)
        {
            // Usamos el método de Box-Muller para transformar dos números uniformes en una Normal
            // Necesitamos dos números aleatorios uniformes [0,1]
            double u1 = generator.SiguienteUniforme();
            double u2 = generator.SiguienteUniforme();

            // Si el generador devuelve 0, evitamos log(0) que es infinito
            if (u1 == 0) u1 = 0.0001;

            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return media + (z * desviacionEstandar);
        }
    }
}