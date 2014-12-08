using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    public class ReporteEquipamientoEscuelas
    {
        [Display(Name = "Número")]
        public int EscuelaNumero { get; set; }
        [Display(Name = "Nombre")]
        public string EscuelaNombre { get; set; }
        [Display(Name = "Máquinas Funcionan")]
        public int CantMaquinasFunc { get; set; }
        [Display(Name = "Máquinas NO Funcionan")]
        public int CantMaquinasNoFunc { get; set; }
        [Display(Name = "Dispositivos de Red")]
        public int CantDispositivosRed { get; set; }
        [Display(Name = "Otros Dispositivos")]
        public int CantOtrosDispositivos { get; set; }
        [Display(Name = "Internet")]
        public string Internet { get; set; }
        [Display(Name = "Tiene ADM")]
        public string TieneADM { get; set; }
        [Display(Name = "Servicios")]
        public int CantServicios { get; set; }
        [Display(Name = "Región")]
        public int DistritoRegion { get; set; }
        [Display(Name = "Distrito")]
        public string DistritoNombre { get; set; }

    }
}
