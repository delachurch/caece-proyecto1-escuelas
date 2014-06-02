using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Relevamiento
    {
        public Relevamiento()
        {
            Maquinas = new List<Maquina>();
            Servicios = new List<Servicio>();
            Dispositivos = new List<Dispositivo>();
            DispositivosRed = new List<DispositivoRed>();
        }
        public int ID { get; set; }
        public Escuela Escuela { get; set; }
        [Display(Name = "¿Tiene ADM?")]
        public string TieneADM { get; set; }
        [Display(Name = "Atendido Por")]
        public string AtendidoPor { get; set; }
        [Display(Name = "Fecha Relevo")]
        public DateTime FechaRelevo { get; set; }
        [Display(Name = "Fecha Modificación")]
        public DateTime? FechaModificacion { get; set; }
        public List<Maquina> Maquinas { get; set; }
        public List<Dispositivo> Dispositivos { get; set; }
        public List<DispositivoRed> DispositivosRed { get; set; }
        public List<Servicio> Servicios { get; set; }

    }
}
