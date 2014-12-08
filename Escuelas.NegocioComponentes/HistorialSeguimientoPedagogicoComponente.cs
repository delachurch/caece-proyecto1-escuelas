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

        public string ConstruirHistorialSegPedagogico(List<SeguimientoPedagogico> SeguimientoPedagogicos)
        {

            if (SeguimientoPedagogicos.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (SeguimientoPedagogico elem in SeguimientoPedagogicos)
                {
                    sb.AppendLine(elem.FechaAlta + " - " + elem.UserProfile.UserName);
                    sb.AppendLine(elem.Comentarios + "\n\n");
                }

                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


    }
}