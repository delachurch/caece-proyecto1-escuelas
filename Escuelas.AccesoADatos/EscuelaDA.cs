using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class EscuelaDA
    {
        public List<Escuela> ObtenerEscuelas()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Escuelas.Include("Distrito").Include("TipoEstablecimiento").ToList();
            }
        }
        public List<Escuela> ObtenerEscuelasPorDistrito(int distId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Escuelas.Include("Distrito").Include("TipoEstablecimiento").Where(e => e.Distrito.ID == distId).ToList();
            }
        }
        public Escuela ObtenerEscuelaPorId(int escuelaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Escuelas.Include("Distrito").Where(e => e.ID == escuelaId).SingleOrDefault();
            }
        }
        public void InsertarEscuela(Escuela nuevaEscuela)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Distritos.Attach(nuevaEscuela.Distrito);
                
                contexto.CategoriaValores.Attach(nuevaEscuela.TipoEstablecimiento);
                
                contexto.Escuelas.Add(nuevaEscuela);
                
                contexto.SaveChanges();
            }
        }
        public void ActualizarEscuela(Escuela nuevaEscuela)
        {
            using (Contexto contexto = new Contexto())
            {
                Escuela escuela = contexto.Escuelas.Include("Distrito").Where(ua => ua.ID == nuevaEscuela.ID).SingleOrDefault();

                if (escuela.Distrito.ID != nuevaEscuela.Distrito.ID)
                {
                    escuela.Distrito = nuevaEscuela.Distrito;
                    contexto.Distritos.Attach(nuevaEscuela.Distrito);
                }

                escuela.Nombre = nuevaEscuela.Nombre;
                escuela.Numero = nuevaEscuela.Numero;
                escuela.Direccion = nuevaEscuela.Direccion;

                contexto.Entry(escuela).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();
            
            }
        }
    }
}
