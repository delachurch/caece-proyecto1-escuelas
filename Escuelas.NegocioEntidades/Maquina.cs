using System;
using System.Collections.Generic;
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
        public string Procesador { get; set; }
        public string MemoriaRAM { get; set; }
        public string CapacidadDisco { get; set; }
        public string SistemaOperativo { get; set; }
        public string DispositivoSonido { get; set; }
        public bool TieneMicrofono { get; set; }
        public string Comentarios { get; set; }
        public Relevamiento Relevamiento { get; set; }
        public string PlacaVideo { get; set; }
        public bool PerteneceARed { get; set; }
    }
}
