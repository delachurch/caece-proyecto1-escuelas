using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escuelas.UI.Models
{
    public class InformeModel
    {
        [Display(Name = "Seleccione Informe")]
        public int TipoInforme { get; set; }
        [Display(Name = "Región")]
        public int Region { get; set; }
        [Display(Name = "Distrito")]
        public int DistritoID { get; set; }
        [Display(Name = "Escuela")]
        public int EscuelaID { get; set; }
    }
}