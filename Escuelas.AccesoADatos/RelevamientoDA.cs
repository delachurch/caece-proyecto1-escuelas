using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class RelevamientoDA
    {
        public List<Relevamiento> ObtenerRelevamientos()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").ToList();
            }
        }

        public Relevamiento ObtenerRelevamientoPorId(int relevamientoId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("Maquinas").Include("Dispositivos.TipoDispositivo").Include("Servicios.TipoServicio").Include("DispositivosRed.TipoDispositivoRed").Where(r => r.ID == relevamientoId).SingleOrDefault();
            }
        }
        public Relevamiento ObtenerUltimoRelevamiento(int escuelaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("Maquinas").Include("Dispositivos.TipoDispositivo").Include("Servicios.TipoServicio").Include("DispositivosRed.TipoDispositivoRed").Where(r => r.Escuela.ID == escuelaId).OrderByDescending(r => r.FechaRelevo).First();
            }
        }
        public List<Relevamiento> ObtenerRelevamientosPorDistrito(int distId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Where(r => r.Escuela.Distrito.ID == distId).ToList();
            }
        }

        public List<Relevamiento> ObtenerRelevamientosPorEscuela(int escId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Where(r => r.Escuela.ID == escId).ToList();
            }
        }

        public void InsertarRelevamiento(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Escuelas.Attach(nuevoRelevamiento.Escuela);

                contexto.Relevamientos.Add(nuevoRelevamiento);

                contexto.SaveChanges();
            }
        }

        public void CopiarRelevamiento(Relevamiento relevamiento)
        {
            Relevamiento nuevoRelevamiento = new Relevamiento();

            nuevoRelevamiento.Escuela = relevamiento.Escuela;
            nuevoRelevamiento.TieneADM = relevamiento.TieneADM;
            nuevoRelevamiento.AtendidoPor = relevamiento.AtendidoPor;
            nuevoRelevamiento.FechaRelevo = DateTime.Now;

            using (Contexto contexto = new Contexto())
            {                

                contexto.Escuelas.Attach(nuevoRelevamiento.Escuela);

                foreach (Maquina maq in relevamiento.Maquinas)
                {
                    Maquina nuevaMaquina = new Maquina();

                    nuevaMaquina.Nombre = maq.Nombre;
                    nuevaMaquina.Marca = maq.Marca;
                    nuevaMaquina.SistemaOperativo = maq.SistemaOperativo;
                    nuevaMaquina.Procesador = maq.Procesador;
                    nuevaMaquina.Monitor = maq.Monitor;
                    nuevaMaquina.CapacidadDiscoDuro = maq.CapacidadDiscoDuro;
                    nuevaMaquina.Comentarios = maq.Comentarios;
                    nuevaMaquina.DispositivoSonido = maq.DispositivoSonido;
                    nuevaMaquina.Disquetera = maq.Disquetera;
                    nuevaMaquina.GrupoDeTrabajo = maq.GrupoDeTrabajo;
                    nuevaMaquina.IP = maq.IP;
                    nuevaMaquina.LectoraCDDVD = maq.LectoraCDDVD;
                    nuevaMaquina.MemoriaRAM = maq.MemoriaRAM;
                    nuevaMaquina.PerteneceARed = maq.PerteneceARed;
                    nuevaMaquina.PlacaVideo = maq.PlacaVideo;
                    nuevaMaquina.TieneMicrofono = maq.TieneMicrofono;
                    nuevaMaquina.Ubicacion = maq.Ubicacion;
                    nuevaMaquina.Webcam = maq.Webcam;

                    nuevoRelevamiento.Maquinas.Add(nuevaMaquina);
                }

                foreach (DispositivoRed disRed in relevamiento.DispositivosRed)
                {
                    DispositivoRed nuevoDispositivoRed = new DispositivoRed();

                    nuevoDispositivoRed.Descripcion = disRed.Descripcion;
                    nuevoDispositivoRed.Marca = disRed.Marca;
                    nuevoDispositivoRed.Modelo = disRed.Modelo;
                    nuevoDispositivoRed.Protocolo = disRed.Protocolo;
                    nuevoDispositivoRed.PuertosUtilizados = disRed.PuertosUtilizados;
                    nuevoDispositivoRed.TipoDispositivoRed = disRed.TipoDispositivoRed;
                    nuevoDispositivoRed.Ubicacion = disRed.Ubicacion;

                    nuevoRelevamiento.DispositivosRed.Add(nuevoDispositivoRed);
                }

                foreach (Dispositivo dis in relevamiento.Dispositivos)
                {
                    Dispositivo nuevoDispositivo = new Dispositivo();

                    nuevoDispositivo.Descripcion = dis.Descripcion;
                    nuevoDispositivo.Marca = dis.Marca;
                    nuevoDispositivo.Modelo = dis.Modelo;
                    nuevoDispositivo.Ubicacion = dis.Ubicacion;
                    nuevoDispositivo.TipoDispositivo = dis.TipoDispositivo;

                    nuevoRelevamiento.Dispositivos.Add(nuevoDispositivo);
                }

                foreach (Servicio ser in relevamiento.Servicios)
                {
                    Servicio nuevoServicio = new Servicio();

                    nuevoServicio.Descripcion = ser.Descripcion;
                    nuevoServicio.TipoServicio = ser.TipoServicio;
                    nuevoServicio.Compañia = ser.Compañia;
                    nuevoServicio.EsPago = ser.EsPago;

                    nuevoRelevamiento.Servicios.Add(nuevoServicio);
                }

                contexto.Relevamientos.Add(nuevoRelevamiento);

                contexto.SaveChanges();

                relevamiento.ID = nuevoRelevamiento.ID;
            }
        }

        public void ActualizarRelevamiento(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {
                Relevamiento relevamiento = contexto.Relevamientos.Include("Escuela").Where(r => r.ID == nuevoRelevamiento.ID).SingleOrDefault();

                if (relevamiento.Escuela.ID != nuevoRelevamiento.Escuela.ID)
                {
                    relevamiento.Escuela = nuevoRelevamiento.Escuela;
                    contexto.Escuelas.Attach(relevamiento.Escuela);
                }

                relevamiento.FechaModificacion = nuevoRelevamiento.FechaModificacion;
                relevamiento.TieneADM = nuevoRelevamiento.TieneADM;
                relevamiento.AtendidoPor = nuevoRelevamiento.AtendidoPor;

                contexto.Entry(relevamiento).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }
  
    }
}
