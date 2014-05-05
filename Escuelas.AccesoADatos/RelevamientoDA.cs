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

        public void InsertarRelevamiento(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Escuelas.Attach(nuevoRelevamiento.Escuela);

                contexto.Relevamientos.Add(nuevoRelevamiento);

                contexto.SaveChanges();
            }
        }
        public void ActualizarRelevamiento(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {
                Relevamiento relevamiento = contexto.Relevamientos.Include("Escuela").Where(r => r.ID == nuevoRelevamiento.ID).SingleOrDefault();

                if (relevamiento.Escuela.ID != nuevoRelevamiento.Escuela.ID)
                {
                    relevamiento.Escuela = nuevoRelevamiento.Escuela;
                    contexto.Escuelas.Attach(relevamiento.Escuela);
                }

                relevamiento.CantMaquinas = nuevoRelevamiento.CantMaquinas;
                relevamiento.Comentarios = nuevoRelevamiento.Comentarios;

                contexto.Entry(relevamiento).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }
     
    }
}
