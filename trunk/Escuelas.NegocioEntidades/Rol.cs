using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Rol
    {
        public Rol()
        {
            Usuarios = new List<Usuario>();
        }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
