using System;
using System.Collections.Generic;
using SimulacionTFI.Domain.Interfaces;

namespace SimulacionTFI.Infrastructure.Generations
{
    public class CongruencialMixto : IGenerator
    {
        private readonly long _a;
        private readonly long _c;
        private readonly long _m;
        private long _xActual;

        // Constructor estándar con valores POSIX
        public CongruencialMixto(long? semilla = null)
        {
            _a = 1103515245;
            _c = 12345;
            _m = 2147483648; // 2^31

            // Usa la semilla provista, o el reloj del sistema si no hay
            _xActual = semilla ?? DateTime.Now.Ticks % _m;

            if (_xActual < 0) _xActual *= -1;
        }

        // Para generar un solo número U(0,1)
        public double SiguienteUniforme()
        {
            _xActual = ((_a * _xActual) + _c) % _m;
            return (double)_xActual / _m;
        }

        // Para generar un lote de números (útil si luego quieren exportarlos para validar en Excel)
        public List<double> GenerarLote(int cantidad)
        {
            var lote = new List<double>(cantidad);
            for (int i = 0; i < cantidad; i++)
            {
                lote.Add(SiguienteUniforme());
            }
            return lote;
        }
    }
}