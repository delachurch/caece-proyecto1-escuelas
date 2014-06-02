using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class DispositivoRed
    {
        public int ID { get; set; }
        public Relevamiento Relevamiento { get; set; }
        [Display(Name = "Tipo Dispositivo Red")]
        public CategoriaValor TipoDispositivoRed { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Puertos utilizados")]
        public int PuertosUtilizados { get; set; }
        public string Protocolo { get; set; }
    }
}
