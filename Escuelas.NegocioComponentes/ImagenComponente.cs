using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class ImagenComponente
    {

        ImagenDA ImagenDA = new ImagenDA();

        public Imagen ObtenerImagenPorId(int imgId)
        {
            return ImagenDA.ObtenerImagenPorId(imgId);
        }

        public void GuardarImagen(Imagen Imagen)
        {

            if (Imagen.ID > 0)
            {
                ImagenDA.ActualizarImagen(Imagen);
            }
            else
            {
                ImagenDA.InsertarImagen(Imagen);
            }
        }

        public void BorrarImagen(int serId)
        {
            ImagenDA.BorrarImagen(serId);
        }
    
    }
}
