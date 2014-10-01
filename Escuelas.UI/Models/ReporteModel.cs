using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Escuelas.UI.Models
{
    public class RelevamientoporEscuela
    {
        public int Anio { get; set; }
        public Escuelas.NegocioEntidades.Escuela Escuela { get; set; }
        public int Total { get; set; }
    }
}