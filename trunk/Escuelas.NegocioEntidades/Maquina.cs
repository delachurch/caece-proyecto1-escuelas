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
        public bool TieneMicrofono { get; set; }
        public string Comentarios { get; set; }
        public Relevamiento Relevamiento { get; set; }
        [Display(Name = "Placa de Video")]
        public string PlacaVideo { get; set; }
        [Display(Name = "¿Pertenece a Red?")]
        public bool PerteneceARed { get; set; }
    }
}
