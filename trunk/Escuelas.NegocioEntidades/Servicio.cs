using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Tipo Servicio")]
        public CategoriaValor TipoServicio { get; set; }
        [Display(Name = "¿Es Pago?")]
        public bool EsPago { get; set; }
        public Relevamiento Relevamiento { get; set; }
    }
}
