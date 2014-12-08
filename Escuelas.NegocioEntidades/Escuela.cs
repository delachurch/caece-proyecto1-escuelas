using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            HistorialComentarios = new List<HistorialComentario>();
            SeguimientoPedagogicos = new List<SeguimientoPedagogico>();
        }
        public int ID { get; set; }
        public Distrito Distrito { get; set; }
        [Display(Name = "Tipo Establecimiento")]
        public CategoriaValor TipoEstablecimiento { get; set; }
        [Required(ErrorMessage = "Debe Ingresar una dirección")]
        public string Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un número")]
        [Display(Name = "Número")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Director")]
        public string Director { get; set; }
        [Display(Name = "Vice Director")]
        public string ViceDirector { get; set; }
        public bool Activa { get; set; }
        public List<Relevamiento> Relevamientos { get; set; }
        public List<Personal> ListaPersonal { get; set; }
        public List<HistorialComentario> HistorialComentarios { get; set; }
        public List<SeguimientoPedagogico> SeguimientoPedagogicos { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}
