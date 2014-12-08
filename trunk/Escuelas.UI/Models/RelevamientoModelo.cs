using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuelas.UI.Models
{
    [Serializable]
    public class RelevamientoModelo
    {
        public Relevamiento Relevamiento { get; set; }
        public string EncId { get; set; }
        public List<MaquinaModel> ListaMaquinas { get; set; }
        public List<DispositivoModel> ListaDispositivos { get; set; }
        public List<SoftwareModel> ListaSoftwares { get; set; }
        public List<CapacitacionModel> ListaCapacitaciones { get; set; }
        public List<ImagenModel> ListaImagenes { get; set; }
        public List<DispositivoRedModel> ListaDispositivosRed { get; set; }
        public List<ServicioModel> ListaServicios { get; set; }
        public Maquina Maquina { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public Software Software{ get; set; }
        public Capacitacion Capacitacion { get; set; }
        public Imagen Imagen { get; set; }
        public DispositivoRed DispositivoRed { get; set; }
        public Servicio Servicio { get; set; }
        public string HistorialComentarios { get; set; }
        public string HistorialSegPedagogico { get; set; }
        public string SeguimientoPedagogico { get; set; }
        public string Comentarios { get; set; }
        public int TabActivo { get; set; }

    }

    public class MaquinaModel
    {
        public Maquina Maquina { get; set; }
        public string EncId { get; set; }
    }
    public class DispositivoModel
    {
        public Dispositivo Dispositivo { get; set; }
        public string EncId { get; set; }
    }
    public class DispositivoRedModel
    {
        public DispositivoRed DispositivoRed { get; set; }
        public string EncId { get; set; }
    }
    public class SoftwareModel
    {
        public Software Software { get; set; }
        public string EncId { get; set; }
    }
    public class ServicioModel
    {
        public Servicio Servicio{ get; set; }
        public string EncId { get; set; }
    }
    public class CapacitacionModel
    {
        public Capacitacion Capacitacion { get; set; }
        public string EncId { get; set; }
    }
    public class ImagenModel
    {
        public Imagen Imagen{ get; set; }
        public string EncId { get; set; }
    }
}

