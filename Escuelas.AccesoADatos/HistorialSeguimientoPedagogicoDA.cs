using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class SeguimientoPedagogicoDA
    {
        public List<SeguimientoPedagogico> ObtenerSeguimientoPedagogicoPorEscuela(int escId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.SeguimientoPedagogicos.Include("UserProfile").Where(h => h.Escuela.ID == escId).OrderByDescending(h => h.FechaAlta).ToList();
            }
        }

        public void InsertarSegPedagogico(SeguimientoPedagogico nuevoSeguimientoPedagogico)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Escuelas.Attach(nuevoSeguimientoPedagogico.Escuela);

                contexto.UserProfiles.Attach(nuevoSeguimientoPedagogico.UserProfile);

                contexto.SeguimientoPedagogicos.Add(nuevoSeguimientoPedagogico);

                contexto.SaveChanges();
            }
        }
    }
}
