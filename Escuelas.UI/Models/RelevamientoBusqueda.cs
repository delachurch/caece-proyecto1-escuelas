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
        public List<RelevamientoEncID> Relevamientos { get; set; }
         [Display(Name = "Distrito")]
        public string DistritoId { get; set; }
         [Display(Name = "Escuela")]
        public string EscuelaId { get; set; }
    }

    public class RelevamientoEncID
    {
        public Relevamiento Relevamiento { get; set; }
        public string EncId { get; set; }
    }

}