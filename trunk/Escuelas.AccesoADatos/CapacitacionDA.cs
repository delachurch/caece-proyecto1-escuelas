using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class CapacitacionDA
    {
        public Capacitacion ObtenerCapacitacionPorId(int capId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Capacitaciones.Where(s => s.ID == capId).SingleOrDefault();
            }
        }

        public void InsertarCapacitacion(Capacitacion nuevaCapacitacion)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevaCapacitacion.Relevamiento);

                contexto.Capacitaciones.Add(nuevaCapacitacion);

                contexto.SaveChanges();
            }
        }

        public void ActualizarCapacitacion(Capacitacion nuevaCapacitacion)
        {
            using (Contexto contexto = new Contexto())
            {
                Capacitacion Capacitacion = contexto.Capacitaciones.Include("Relevamiento").Where(c => c.ID == nuevaCapacitacion.ID).SingleOrDefault();


                Capacitacion.Curso = nuevaCapacitacion.Curso;
                Capacitacion.Grado = nuevaCapacitacion.Grado;
                Capacitacion.Descripcion = nuevaCapacitacion.Descripcion;


                contexto.Entry(Capacitacion).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }
        public void BorrarCapacitacion(int capId)
        {
            using (Contexto contexto = new Contexto())
            {
                Capacitacion Capacitacion = contexto.Capacitaciones.Where(c => c.ID == capId).SingleOrDefault();

                contexto.Capacitaciones.Remove(Capacitacion);

                contexto.SaveChanges();
            }
        }
    }
}
