using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Maquina
    {
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Procesador { get; set; }
        [Display(Name = "RAM")]
        public string MemoriaRAM { get; set; }
        [Display(Name = "Disco Duro")]
        public string CapacidadDiscoDuro { get; set; }
        [Display(Name = "Sist. Operativo")]
        public string SistemaOperativo { get; set; }
        [Display(Name = "Dispositivo de Sonido")]
        public string DispositivoSonido { get; set; }
        [Display(Name = "¿Tiene Microfono?")]
        public string TieneMicrofono { get; set; }
        public string Comentarios { get; set; }
        public Relevamiento Relevamiento { get; set; }
        [Display(Name = "Placa de Video")]
        public string PlacaVideo { get; set; }
        [Display(Name = "¿Pertenece a Red?")]
        public string PerteneceARed { get; set; }
        [Display(Name = "CD")]
        public string CDLectGrab { get; set; }
        [Display(Name = "¿Funciona?")]
        public string Funciona { get; set; }
        [Display(Name = "DVD")]
        public string DVDLectGrab { get; set; }
        public string Disquetera{ get; set; }
        public string Monitor { get; set; }
        [Display(Name = "¿Tiene Webcam?")]
        public string Webcam { get; set; }
        [Display(Name = "Grupo de trabajo")]
        public string GrupoDeTrabajo { get; set; }
        public string IP { get; set; }
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
        [Display(Name = "Puertos USB Frontales")]
        public int PuertosUSBFrontales { get; set; }
        [Display(Name = "Puertos USB Traseros")]
        public int PuertosUSBTraseros { get; set; }

    }
}
