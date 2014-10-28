using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class SeguimientoPedagogico
    {
        public int ID { get; set; }
        public Escuela Escuela { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
