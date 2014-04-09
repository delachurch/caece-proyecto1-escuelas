using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Servicio
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public CategoriaValor TipoServicio { get; set; }
        public bool EsPago { get; set; }
        public Relevamiento Relevamiento { get; set; }
    }
}
