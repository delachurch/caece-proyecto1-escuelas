using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class SoftwareDA
    {
        public Software ObtenerSoftwarePorId(int softId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Softwares.Include("TipoSoftware").Where(s => s.ID == softId).SingleOrDefault();
            }
        }

        public void InsertarSoftware(Software nuevoSoftware)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevoSoftware.Relevamiento);

                contexto.CategoriaValores.Attach(nuevoSoftware.TipoSoftware);

                contexto.Softwares.Add(nuevoSoftware);

                contexto.SaveChanges();
            }
        }

        public void ActualizarSoftware(Software nuevoSoftware)
        {
            using (Contexto contexto = new Contexto())
            {
                Software Software = contexto.Softwares.Include("Relevamiento").Include("TipoSoftware").Where(d => d.ID == nuevoSoftware.ID).SingleOrDefault();


                Software.Descripcion = nuevoSoftware.Descripcion;
                Software.Nombre = nuevoSoftware.Nombre;

                if (Software.TipoSoftware.ID != nuevoSoftware.TipoSoftware.ID)
                {
                    Software.TipoSoftware = nuevoSoftware.TipoSoftware;
                    contexto.CategoriaValores.Attach(Software.TipoSoftware);
                }

                contexto.Entry(Software).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarSoftware(int disId)
        {
            using (Contexto contexto = new Contexto())
            {
                Software Software = contexto.Softwares.Where(d => d.ID == disId).SingleOrDefault();

                contexto.Softwares.Remove(Software);

                contexto.SaveChanges();
            }
        }
    }
}
