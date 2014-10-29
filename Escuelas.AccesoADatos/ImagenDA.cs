using Escuelas.NegocioEntidades;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class ImagenDA
    {

        public Imagen ObtenerImagenPorId(int imgId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Imagenes.Where(s => s.ID == imgId).SingleOrDefault();
            }
        }

        public void InsertarImagen(Imagen nuevaImagen)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevaImagen.Relevamiento);

                contexto.Imagenes.Add(nuevaImagen);

                contexto.SaveChanges();
            }
        }

        public void ActualizarImagen(Imagen nuevaImagen)
        {
            using (Contexto contexto = new Contexto())
            {
                Imagen Imagen = contexto.Imagenes.Include("Relevamiento").Where(c => c.ID == nuevaImagen.ID).SingleOrDefault();

                Imagen.Contenido = nuevaImagen.Contenido;
                Imagen.Descripcion = nuevaImagen.Descripcion;
                Imagen.Foto = nuevaImagen.Foto;
                Imagen.Relevamiento = nuevaImagen.Relevamiento;
                Imagen.Titulo = nuevaImagen.Titulo;
                                
                contexto.Entry(Imagen).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarImagen(int capId)
        {
            using (Contexto contexto = new Contexto())
            {
                Imagen Imagen = contexto.Imagenes.Where(c => c.ID == capId).SingleOrDefault();

                contexto.Imagenes.Remove(Imagen);

                contexto.SaveChanges();
            }
        }
        
    }
}
