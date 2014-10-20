using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Capacitacion
    {
        public int ID { get; set; }
        public Relevamiento Relevamiento { get; set; }
        public int Grado { get; set; }
        public string Curso { get; set; }
        public string Descripcion { get; set; }
    }
}
