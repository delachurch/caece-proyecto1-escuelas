using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Dispositivo
    {
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public Relevamiento Relevamiento { get; set; }
        [Display(Name = "Tipo Dispositivo")]
        public CategoriaValor TipoDispositivo { get; set; }
    }
}
