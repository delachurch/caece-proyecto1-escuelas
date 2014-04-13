using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Distrito
    {
        public Distrito()
        {
            Escuelas = new List<Escuela>();
            ListaPersonal = new List<Personal>();
            Usuarios = new List<Usuario>();
        }
        public int ID { get; set; }
        public int Region { get; set; }
        [Display(Name = "Distrito")]
        public string Nombre { get; set; }
        public List<Escuela> Escuelas { get; set; }
        public List<Personal> ListaPersonal { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
