using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class CategoriaValor
    {
        public CategoriaValor()
        {
            Escuelas = new List<Escuela>();
            Servicios = new List<Servicio>();
            Dispositivos = new List<Dispositivo>();
            DispositivosRed = new List<DispositivoRed>();
            Softwares = new List<Software>();
            SoftwaresDePlataforma = new List<Software>();
        }
        public int ID { get; set; }
        public Categoria Categoria { get; set; }
        public string Nombre { get; set; }
        public List<Escuela> Escuelas { get; set; }
        public List<Servicio> Servicios { get; set; }
        public List<Dispositivo> Dispositivos { get; set; }
        public List<DispositivoRed> DispositivosRed { get; set; }
        public List<Software> Softwares { get; set; }
        public List<Software> SoftwaresDePlataforma { get; set; }

    }
}
