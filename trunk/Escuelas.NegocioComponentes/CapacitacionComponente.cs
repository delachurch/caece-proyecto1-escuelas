using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class CapacitacionComponente
    {
        CapacitacionDA CapacitacionDA = new CapacitacionDA();

        public Capacitacion ObtenerCapacitacionPorId(int serId)
        {
            return CapacitacionDA.ObtenerCapacitacionPorId(serId);
        }

        public void GuardarCapacitacion(Capacitacion Capacitacion)
        {

            if (Capacitacion.ID > 0)
            {
                CapacitacionDA.ActualizarCapacitacion(Capacitacion);
            }
            else
            {
                CapacitacionDA.InsertarCapacitacion(Capacitacion);
            }
        }

        public void BorrarCapacitacion(int serId)
        {
            CapacitacionDA.BorrarCapacitacion(serId);
        }
    }
}
