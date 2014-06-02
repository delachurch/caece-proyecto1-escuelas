using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class ServicioDA
    {
        public Servicio ObtenerServicioPorId(int serId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Servicios.Include("TipoServicio").Where(s => s.ID == serId).SingleOrDefault();
            }
        }

        public void InsertarServicio(Servicio nuevoServicio)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevoServicio.Relevamiento);

                contexto.CategoriaValores.Attach(nuevoServicio.TipoServicio);

                contexto.Servicios.Add(nuevoServicio);

                contexto.SaveChanges();
            }
        }

        public void ActualizarServicio(Servicio nuevoServicio)
        {
            using (Contexto contexto = new Contexto())
            {
                Servicio servicio = contexto.Servicios.Include("Relevamiento").Include("TipoServicio").Where(d => d.ID == nuevoServicio.ID).SingleOrDefault();


                servicio.EsPago = nuevoServicio.EsPago;
                servicio.Compañia = nuevoServicio.Compañia;
                servicio.Descripcion = nuevoServicio.Descripcion;

                if (servicio.TipoServicio.ID != nuevoServicio.TipoServicio.ID)
                {
                    servicio.TipoServicio = nuevoServicio.TipoServicio;
                    contexto.CategoriaValores.Attach(servicio.TipoServicio);
                }

                contexto.Entry(servicio).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }
        public void BorrarServicio(int serId)
        {
            using (Contexto contexto = new Contexto())
            {
                Servicio servicio = contexto.Servicios.Where(s => s.ID == serId).SingleOrDefault();

                contexto.Servicios.Remove(servicio);

                contexto.SaveChanges();
            }
        }
    }
}
