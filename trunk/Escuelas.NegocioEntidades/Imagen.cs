using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Imagen
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [AllowHtml]
        public string Contenido { get; set; }
        public byte[] Foto { get; set; }
        public Relevamiento Relevamiento { get; set; }


    }
}
