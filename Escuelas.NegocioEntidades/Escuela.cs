using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Escuela
    {
        public Escuela()
        {
            Relevamientos = new List<Relevamiento>();
            ListaPersonal = new List<Personal>();
        }
        public int ID { get; set; }
        public Distrito Distrito { get; set; }
        public CategoriaValor TipoEstablecimiento { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int Numero { get; set; }
        public string Nombre { get; set; }
        public List<Relevamiento> Relevamientos { get; set; }
        public List<Personal> ListaPersonal { get; set; }

    }
}
