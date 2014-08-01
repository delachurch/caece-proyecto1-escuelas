using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class HistorialComentarioDA
    {
        public List<HistorialComentario> ObtenerHistorialComentarioPorEscuela(int escId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.HistorialComentarios.Include("UserProfile").Where(h => h.Escuela.ID == escId).OrderByDescending(h => h.FechaAlta).ToList();
            }
        }

        public void InsertarComentario(HistorialComentario nuevoHistorialComentario)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Escuelas.Attach(nuevoHistorialComentario.Escuela);

                contexto.UserProfiles.Attach(nuevoHistorialComentario.UserProfile);

                contexto.HistorialComentarios.Add(nuevoHistorialComentario);

                contexto.SaveChanges();
            }
        }
    }
}
