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
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("CreadoPor").Include("ModificadoPor").OrderByDescending(r => r.FechaRelevo).ToList();
            }
        }

        public Relevamiento ObtenerRelevamientoPorId(int relevamientoId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("Maquinas").Include("Dispositivos.TipoDispositivo").Include("Servicios.TipoServicio").Include("DispositivosRed.TipoDispositivoRed").Include("Softwares.TipoSoftware").Include("Softwares.PlataformaSoftware").Include("Capacitaciones").Where(r => r.ID == relevamientoId).SingleOrDefault();
            }
        }
        public Relevamiento ObtenerUltimoRelevamiento(int escuelaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("Maquinas").Include("Dispositivos.TipoDispositivo.Categoria").Include("Servicios.TipoServicio.Categoria").Include("DispositivosRed.TipoDispositivoRed.Categoria").Include("Softwares.TipoSoftware").Include("Softwares.PlataformaSoftware").Include("Capacitaciones").Where(r => r.Escuela.ID == escuelaId).OrderByDescending(r => r.FechaRelevo).First();
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
                return contexto.Relevamientos.Include("Escuela.Distrito").Include("CreadoPor").Include("ModificadoPor")
                    .Where(r => r.Escuela.ID == escId)
                    .OrderByDescending(r => r.FechaRelevo)
                    .ToList();
            }
        }

        public void InsertarRelevamiento(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Escuelas.Attach(nuevoRelevamiento.Escuela);

                contexto.UserProfiles.Attach(nuevoRelevamiento.CreadoPor);

                contexto.Relevamientos.Add(nuevoRelevamiento);

                contexto.SaveChanges();
            }
        }

        public void CopiarRelevamiento(Relevamiento relevamiento, UserProfile creadoPor)
        {
            Relevamiento nuevoRelevamiento = new Relevamiento();

            nuevoRelevamiento.Escuela = new Escuela { ID = relevamiento.Escuela.ID };
            nuevoRelevamiento.TieneADM = relevamiento.TieneADM;
            nuevoRelevamiento.AtendidoPor = relevamiento.AtendidoPor;
            nuevoRelevamiento.FechaRelevo = DateTime.Now;
            nuevoRelevamiento.CreadoPor = new UserProfile { UserId = creadoPor.UserId };
            using (Contexto contexto = new Contexto())
            {                

                contexto.Escuelas.Attach(nuevoRelevamiento.Escuela);
                contexto.UserProfiles.Attach(nuevoRelevamiento.CreadoPor);

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
                    nuevaMaquina.PuertosUSBFrontales = maq.PuertosUSBFrontales;
                    nuevaMaquina.PuertosUSBTraseros = maq.PuertosUSBTraseros;
                    nuevoRelevamiento.Maquinas.Add(nuevaMaquina);
                }

                List<CategoriaValor> listaAttacheados = new List<CategoriaValor>();

                foreach (DispositivoRed disRed in relevamiento.DispositivosRed)
                {
                    DispositivoRed nuevoDispositivoRed = new DispositivoRed();

                    nuevoDispositivoRed.Descripcion = disRed.Descripcion;
                    nuevoDispositivoRed.Marca = disRed.Marca;
                    nuevoDispositivoRed.Modelo = disRed.Modelo;
                    nuevoDispositivoRed.Protocolo = disRed.Protocolo;
                    nuevoDispositivoRed.PuertosUtilizados = disRed.PuertosUtilizados;
                    nuevoDispositivoRed.PuertosTotales = disRed.PuertosTotales;


                    CategoriaValor attacheado = listaAttacheados.Find(cv => cv.ID == disRed.TipoDispositivoRed.ID);

                    if (attacheado == null)
                    {
                        nuevoDispositivoRed.TipoDispositivoRed = new CategoriaValor() { ID = disRed.TipoDispositivoRed.ID };

                        contexto.CategoriaValores.Attach(nuevoDispositivoRed.TipoDispositivoRed);

                        listaAttacheados.Add(nuevoDispositivoRed.TipoDispositivoRed);
                    }
                    else
                    {
                        nuevoDispositivoRed.TipoDispositivoRed = attacheado;
                    }
                    
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

                    CategoriaValor attacheado = listaAttacheados.Find(cv => cv.ID == dis.TipoDispositivo.ID);

                    if (attacheado == null)
                    {
                        nuevoDispositivo.TipoDispositivo = new CategoriaValor() { ID = dis.TipoDispositivo.ID };

                        contexto.CategoriaValores.Attach(nuevoDispositivo.TipoDispositivo);

                        listaAttacheados.Add(nuevoDispositivo.TipoDispositivo);
                    }
                    else
                    {
                        nuevoDispositivo.TipoDispositivo = attacheado;
                    }

                    nuevoRelevamiento.Dispositivos.Add(nuevoDispositivo);
                }

                foreach (Software soft in relevamiento.Softwares)
                {
                    Software nuevoSoftware = new Software();

                    nuevoSoftware.Descripcion = soft.Descripcion;
                    nuevoSoftware.Nombre = soft.Nombre;

                    CategoriaValor attacheado = listaAttacheados.Find(cv => cv.ID == soft.TipoSoftware.ID);

                    if (attacheado == null)
                    {
                        nuevoSoftware.TipoSoftware = new CategoriaValor() { ID = soft.TipoSoftware.ID };

                        contexto.CategoriaValores.Attach(nuevoSoftware.TipoSoftware);

                        listaAttacheados.Add(nuevoSoftware.TipoSoftware);
                    }
                    else
                    {
                        nuevoSoftware.TipoSoftware = attacheado;
                    }

                    attacheado = listaAttacheados.Find(cv => cv.ID == soft.PlataformaSoftware.ID);

                    if (attacheado == null)
                    {
                        nuevoSoftware.PlataformaSoftware = new CategoriaValor() { ID = soft.PlataformaSoftware.ID };

                        contexto.CategoriaValores.Attach(nuevoSoftware.PlataformaSoftware);

                        listaAttacheados.Add(nuevoSoftware.PlataformaSoftware);
                    }
                    else
                    {
                        nuevoSoftware.PlataformaSoftware = attacheado;
                    }

                    nuevoRelevamiento.Softwares.Add(nuevoSoftware);
                }

                foreach (Capacitacion cap in relevamiento.Capacitaciones)
                {
                    Capacitacion nuevaCapacitacion = new Capacitacion();

                    nuevaCapacitacion.Curso = cap.Curso;
                    nuevaCapacitacion.Descripcion = cap.Descripcion;

                    nuevoRelevamiento.Capacitaciones.Add(nuevaCapacitacion);
                }

                foreach (Servicio ser in relevamiento.Servicios)
                {
                    Servicio nuevoServicio = new Servicio();

                    nuevoServicio.Descripcion = ser.Descripcion;

                    CategoriaValor attacheado = listaAttacheados.Find(cv => cv.ID == ser.TipoServicio.ID);

                    if (attacheado == null)
                    {
                        nuevoServicio.TipoServicio = new CategoriaValor() { ID = ser.TipoServicio.ID };

                        contexto.CategoriaValores.Attach(nuevoServicio.TipoServicio);

                        listaAttacheados.Add(nuevoServicio.TipoServicio);
                    }
                    else
                    {
                        nuevoServicio.TipoServicio = attacheado;
                    }

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

                if ((relevamiento.ModificadoPor == null) || (relevamiento.ModificadoPor != null && relevamiento.ModificadoPor.UserId != nuevoRelevamiento.ModificadoPor.UserId))
                {
                    relevamiento.ModificadoPor = nuevoRelevamiento.ModificadoPor;
                    contexto.UserProfiles.Attach(relevamiento.ModificadoPor);
                }

                relevamiento.FechaModificacion = nuevoRelevamiento.FechaModificacion;
                relevamiento.TieneADM = nuevoRelevamiento.TieneADM;
                relevamiento.AtendidoPor = nuevoRelevamiento.AtendidoPor;

                contexto.Entry(relevamiento).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void ActualizarFechaYModificadoPor(Relevamiento nuevoRelevamiento)
        {
            using (Contexto contexto = new Contexto())
            {
                Relevamiento relevamiento = contexto.Relevamientos.Include("Escuela").Where(r => r.ID == nuevoRelevamiento.ID).SingleOrDefault();

                if ((relevamiento.ModificadoPor == null) || (relevamiento.ModificadoPor != null && relevamiento.ModificadoPor.UserId != nuevoRelevamiento.ModificadoPor.UserId))
                {
                    relevamiento.ModificadoPor = nuevoRelevamiento.ModificadoPor;
                    contexto.UserProfiles.Attach(relevamiento.ModificadoPor);
                }
                relevamiento.FechaModificacion = nuevoRelevamiento.FechaModificacion;

                contexto.Entry(relevamiento).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

    }
}
