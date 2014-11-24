using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Models
{
    public class DistritoEscuelaModel
    {
        public DistritoEscuelaModel()
        {
            listaDistritos = new List<SelectListItem>();
            listaEscuelas = new List<SelectListItem>();
        }
        public List<SelectListItem> listaDistritos { get; set; }
        public List<SelectListItem> listaEscuelas { get; set; }
    }
}