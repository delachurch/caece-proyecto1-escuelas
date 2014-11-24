using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class DistritoDA
    {
        public List<Distrito> ObtenerDistritos()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Distritos.Where(d => d.Inactivo == false).OrderBy(d => d.Nombre).ToList();
            }
        }

        public List<Distrito> ObtenerDistritosPorRegion(int region)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Distritos.Where(d => d.Inactivo == false && d.Region == region).OrderBy(d => d.Nombre).ToList();
            }
        }

        public Distrito ObtenerDistritoPorId(int distritoId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Distritos.Where(d => d.ID == distritoId).SingleOrDefault();
            }
        }


        public void InsertarDistrito(Distrito nuevoDistrito)
        {
            using (Contexto contexto = new Contexto())
            {
                
                contexto.Distritos.Add(nuevoDistrito);

                contexto.SaveChanges();
            }
        }
        public void ActualizarDistrito(Distrito nuevoDistrito)
        {
            using (Contexto contexto = new Contexto())
            {
                Distrito distrito = contexto.Distritos.Where(d => d.ID == nuevoDistrito.ID).SingleOrDefault();

                distrito.Region = nuevoDistrito.Region;
                distrito.Nombre = nuevoDistrito.Nombre;
                
                contexto.Entry(distrito).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarDistrito(int distritoId)
        {
            using (Contexto contexto = new Contexto())
            {
                Distrito distrito = contexto.Distritos.Where(d => d.ID == distritoId).SingleOrDefault();

                distrito.Inactivo = true;

                contexto.Entry(distrito).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();
            }
        }

    }
}
