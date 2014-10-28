using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Software
    {
        public int ID { get; set; }
        public Relevamiento Relevamiento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Tipo Software")]
        public CategoriaValor TipoSoftware { get; set; }
        [Display(Name = "Ubicación")]
        public CategoriaValor PlataformaSoftware { get; set; }
    }
}
