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
                return contexto.Softwares.Include("TipoSoftware").Include("PlataformaSoftware").Where(s => s.ID == softId).SingleOrDefault();
            }
        }

        public void InsertarSoftware(Software nuevoSoftware)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevoSoftware.Relevamiento);

                contexto.CategoriaValores.Attach(nuevoSoftware.TipoSoftware);

                contexto.CategoriaValores.Attach(nuevoSoftware.PlataformaSoftware);

                contexto.Softwares.Add(nuevoSoftware);

                contexto.SaveChanges();
            }
        }

        public void ActualizarSoftware(Software nuevoSoftware)
        {
            using (Contexto contexto = new Contexto())
            {
                Software Software = contexto.Softwares.Include("Relevamiento").Include("TipoSoftware").Include("PlataformaSoftware").Where(d => d.ID == nuevoSoftware.ID).SingleOrDefault();


                Software.Descripcion = nuevoSoftware.Descripcion;
                Software.Nombre = nuevoSoftware.Nombre;

                if (Software.TipoSoftware.ID != nuevoSoftware.TipoSoftware.ID)
                {
                    Software.TipoSoftware = nuevoSoftware.TipoSoftware;
                    contexto.CategoriaValores.Attach(Software.TipoSoftware);
                }

                if (Software.PlataformaSoftware.ID != nuevoSoftware.PlataformaSoftware.ID)
                {
                    Software.PlataformaSoftware = nuevoSoftware.PlataformaSoftware;
                    contexto.CategoriaValores.Attach(Software.PlataformaSoftware);
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
