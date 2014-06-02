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
        [Display(Name = "Compañía")]
        public string Compañia { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Tipo Servicio")]
        public CategoriaValor TipoServicio { get; set; }
        [Display(Name = "¿Es Pago?")]
        public string EsPago { get; set; }
        public Relevamiento Relevamiento { get; set; }
    }
}
