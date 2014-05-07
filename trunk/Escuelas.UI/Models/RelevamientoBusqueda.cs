using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escuelas.UI.Models
{
    public class RelevamientoBusqueda
    {
        public List<Relevamiento> Relevamientos { get; set; }
         [Display(Name = "Distrito")]
        public int DistritoId { get; set; }
         [Display(Name = "Escuela")]
        public int EscuelaId { get; set; }
    }
}