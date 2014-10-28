using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class SeguimientoPedagogicoComponente
    {
        SeguimientoPedagogicoDA SeguimientoPedagogicoDA = new SeguimientoPedagogicoDA();
        public List<SeguimientoPedagogico> ObtenerSeguimientoPedagogicoPorEscuela(int escId)
        {
            return SeguimientoPedagogicoDA.ObtenerSeguimientoPedagogicoPorEscuela(escId);
        }

        public void GuardarSegPedagogico(SeguimientoPedagogico nuevoSeguimientoPedagogico)
        {
            SeguimientoPedagogicoDA.InsertarSegPedagogico(nuevoSeguimientoPedagogico);
        }

    }
}