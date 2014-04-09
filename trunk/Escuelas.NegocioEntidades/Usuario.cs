using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Rol Rol { get; set; }
        public string Direccion { get; set; }
        public Distrito Distrito { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
