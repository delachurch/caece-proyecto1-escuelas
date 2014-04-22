using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class RelevamientoDA
    {
        public List<Relevamiento> ObtenerRelevamientos()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").ToList();
            }
        }

        public Relevamiento ObtenerRelevamientoPorId(int relevamientoId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("Maquinas").Include("Dispositivos").Include("Servicios").Where(r => r.ID == relevamientoId).SingleOrDefault();
            }
        }

        public List<Relevamiento> ObtenerRelevamientosPorDistrito(int distId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Where(r => r.Escuela.Distrito.ID == distId).ToList();
            }
        }
     
    }
}
