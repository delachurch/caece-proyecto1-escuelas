using Escuelas.Comun;
using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.Seguridad;
using Escuelas.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Newtonsoft.Json;
using System.Text;

namespace relevamientos.UI.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class RelevamientoController : Controller
    {
        //
        // GET: /Relevamiento/
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        HistorialComentarioComponente historialComentarioComponente = new HistorialComentarioComponente();
        SeguimientoPedagogicoComponente SeguimientoPedagogicoComponente = new SeguimientoPedagogicoComponente();
        UsuarioComponente usuarioComponente = new UsuarioComponente();
        MaquinaComponente maquinaComponente = new MaquinaComponente();
        DispositivoComponente dispositivoComponente = new DispositivoComponente();
        SoftwareComponente softwareComponente = new SoftwareComponente();
        DispositivoRedComponente dispositivoRedComponente = new DispositivoRedComponente();
        ServicioComponente servicioComponente = new ServicioComponente();
        CapacitacionComponente capacitacionComponente = new CapacitacionComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult RelevamientoIndex(int? distId, int? escId)
        {
            RelevamientoBusqueda relevamientoBusqueda = new RelevamientoBusqueda();
                    
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            List<Escuela> listaEscuelas;

            if (distId.Value > 0)
            {
                relevamientoBusqueda.DistritoId = distId.Value;
                listaEscuelas = CargarEscuelasPorDistrito(relevamientoBusqueda.DistritoId);
            }
            else
            {
                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);
            }

            

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            if (escId.Value > 0)
            {
                relevamientoBusqueda.EscuelaId = escId.Value;
            }

            relevamientoBusqueda.Relevamientos = relevamientoComponente.ObtenerRelevamientosPorEscuela(relevamientoBusqueda.EscuelaId);

            return View(relevamientoBusqueda);

        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        [HttpPost]
        public ActionResult RelevamientoIndex(RelevamientoBusqueda relevamientoBusqueda)
        {
            return RedirectToAction("RelevamientoIndex", new { distId = relevamientoBusqueda.DistritoId, escId = relevamientoBusqueda.EscuelaId });
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportPDF(int relevamientoId)
        {
            return new ActionAsPdf("ExportarRelevamiento", new { relevamientoId = relevamientoId }) {FileName = "Relevamiento.pdf" };
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportarRelevamiento(int relevamientoId)
        {

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();
            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            return View(relevamientoModelo);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CopiarUltimoRelevamiento(int escuelaId)
        {

            Relevamiento relevamiento = relevamientoComponente.ObtenerUltimoRelevamiento(escuelaId);

            relevamientoComponente.CopiarRelevamiento(relevamiento);


            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamiento.ID, tActivo = 0, mensaje = "Relevamiento copiado exitosamente" });
        }
      
        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult EditarRelevamiento(int relevamientoId, int tActivo, string mensaje)
        {  
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivos = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivo.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoSoftwares = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoSoftware.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivosRed = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivoRed.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoServicios = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoServicio.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaGrados = ObtenerGrados();
            
            ViewBag.Mensaje = mensaje;

            List<Escuela> listaEscuelas;
          
            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();

            relevamientoModelo.Maquina = new Maquina();

            relevamientoModelo.Dispositivo = new Dispositivo();

            relevamientoModelo.Dispositivo.TipoDispositivo = new CategoriaValor();

            relevamientoModelo.Software= new Software();

            relevamientoModelo.Software.TipoSoftware = new CategoriaValor();

            relevamientoModelo.Capacitacion = new Capacitacion();

            relevamientoModelo.Servicio= new Servicio();

            relevamientoModelo.Servicio.TipoServicio = new CategoriaValor();

            relevamientoModelo.DispositivoRed = new DispositivoRed();

            relevamientoModelo.DispositivoRed.TipoDispositivoRed = new CategoriaValor();

            relevamientoModelo.TabActivo = tActivo;

            if (relevamientoId > 0)
            {
             
                relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);

                relevamientoModelo.Relevamiento.Maquinas = relevamientoModelo.Relevamiento.Maquinas.OrderBy(m => m.Nombre).ToList();

                relevamientoModelo.Relevamiento.Servicios = relevamientoModelo.Relevamiento.Servicios.OrderBy(s => s.Compañia).ToList();
 
                listaEscuelas = CargarEscuelasPorDistrito(relevamientoModelo.Relevamiento.Escuela.Distrito.ID);

                ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            }
            else
            {
                relevamientoModelo.Relevamiento = new Relevamiento();

                int escuelaId = 0;
                string director = "";
                string viceDirector = "";

                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);

                if (listaEscuelas.Count() > 0)
                {
                    escuelaId = listaEscuelas.First().ID;
                    director = listaEscuelas.First().Director;
                    viceDirector = listaEscuelas.First().ViceDirector;
                }


                relevamientoModelo.Relevamiento.Escuela = new Escuela() { ID =  escuelaId } ;
                relevamientoModelo.Relevamiento.Escuela.Distrito = new Distrito();
                relevamientoModelo.Relevamiento.Escuela.Director = director;
                relevamientoModelo.Relevamiento.Escuela.ViceDirector = viceDirector;
            }

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));
            
            relevamientoModelo.HistorialComentarios = ConstruirHistorialComentarios(historialComentarioComponente.ObtenerHistorialComentarioPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));

            relevamientoModelo.HistorialSegPedagogico = ConstruirHistorialSegPedagogico(SeguimientoPedagogicoComponente.ObtenerSeguimientoPedagogicoPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));

            return View(relevamientoModelo);

        }

        

        [Authorize(Roles = "Admin,Colaborador")]
        private string ConstruirHistorialSegPedagogico(List<SeguimientoPedagogico> SeguimientoPedagogicos)
        {
            
            if (SeguimientoPedagogicos.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (SeguimientoPedagogico elem in SeguimientoPedagogicos)
                {
                    sb.AppendLine(elem.FechaAlta + " - " + elem.UserProfile.UserName);
                    sb.AppendLine(elem.Comentarios + "\n\n");
                }

                return sb.ToString();
        }
            else
            {
                return string.Empty;
            }
        }

        [Authorize(Roles = "Admin,Colaborador")]
        private string ConstruirHistorialComentarios(List<HistorialComentario> historialComentarios)
        {
            
            if (historialComentarios.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (HistorialComentario elem in historialComentarios)
                {
                    sb.AppendLine(elem.FechaAlta + " - " + elem.UserProfile.UserName);
                    sb.AppendLine(elem.Comentarios + "\n\n");
                }

                return sb.ToString();
        }
            else
            {
                return string.Empty;
            }
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarRelevamiento(RelevamientoModelo relevamientoModelo)
        {

            relevamientoComponente.GuardarRelevamiento(relevamientoModelo.Relevamiento);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Relevamiento.ID, tActivo = 0, mensaje= "Relevamiento guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarMaquina(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Maquina.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            maquinaComponente.GuardarMaquina(relevamientoModelo.Maquina);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Maquina.Relevamiento.ID, tActivo = 1, mensaje = "Maquina guardada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarMaquina(int maqId,int relId)
        {

            maquinaComponente.BorrarMaquina(maqId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 1,  mensaje = "Maquina borrada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarDispositivo(int disId, int relId)
        {

            dispositivoComponente.BorrarDispositivo(disId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 3, mensaje = "Dispositivo borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarSoftware(int softId, int relId)
        {

            softwareComponente.BorrarSoftware(softId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 5, mensaje = "Software borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarDispositivoRed(int disRedId, int relId)
        {

            dispositivoRedComponente.BorrarDispositivoRed(disRedId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 2, mensaje = "Dispositivo de Red borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarServicio(int serId, int relId)
        {

           servicioComponente.BorrarServicio(serId);

           return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 4, mensaje = "Servicio borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarCapacitacion(int capId, int relId)
        {

            capacitacionComponente.BorrarCapacitacion(capId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 6, mensaje = "Capacitacion borrada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarDispositivo(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Dispositivo.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoComponente.GuardarDispositivo(relevamientoModelo.Dispositivo);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Dispositivo.Relevamiento.ID, tActivo = 3, mensaje = "Dispositivo guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarSoftware(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Software.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            softwareComponente.GuardarSoftware(relevamientoModelo.Software);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Software.Relevamiento.ID, tActivo = 5, mensaje = "Software guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarDispositivoRed(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.DispositivoRed.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoRedComponente.GuardarDispositivoRed(relevamientoModelo.DispositivoRed);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.DispositivoRed.Relevamiento.ID, tActivo = 2, mensaje = "Dispositivo de Red guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarSegPedagogico(RelevamientoModelo relevamientoModelo)
        {

            Relevamiento relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            SeguimientoPedagogico SeguimientoPedagogico = new SeguimientoPedagogico();

            SeguimientoPedagogico.Escuela = new Escuela { ID = relevamientoModelo.Relevamiento.Escuela.ID };
            SeguimientoPedagogico.UserProfile = new Escuelas.NegocioEntidades.UserProfile { UserId = UsuarioActual.ObtenerUsuarioActual().UserId };
            SeguimientoPedagogico.FechaAlta = DateTime.Now;
            SeguimientoPedagogico.Comentarios = relevamientoModelo.SeguimientoPedagogico;

            SeguimientoPedagogicoComponente.GuardarSegPedagogico(SeguimientoPedagogico);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamiento.ID, tActivo = 8, mensaje = "Comentario Guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarComentarios(RelevamientoModelo relevamientoModelo)
        {

            Relevamiento relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            HistorialComentario historialComentario = new HistorialComentario();

            historialComentario.Escuela = new Escuela { ID = relevamientoModelo.Relevamiento.Escuela.ID };
            historialComentario.UserProfile = new Escuelas.NegocioEntidades.UserProfile { UserId = UsuarioActual.ObtenerUsuarioActual().UserId };
            historialComentario.FechaAlta = DateTime.Now;
            historialComentario.Comentarios = relevamientoModelo.Comentarios;

            historialComentarioComponente.GuardarComentario(historialComentario);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamiento.ID, tActivo = 7, mensaje = "Comentario Guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarServicio(RelevamientoModelo relevamientoModelo)
        {


            relevamientoModelo.Servicio.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            servicioComponente.GuardarServicio(relevamientoModelo.Servicio);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Servicio.Relevamiento.ID, tActivo = 4, mensaje = "Servicio guardado" });
        }

        [HttpPost]
        public ActionResult EditarCapacitacion(RelevamientoModelo relevamientoModelo)
        {


            relevamientoModelo.Capacitacion.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            capacitacionComponente.GuardarCapacitacion(relevamientoModelo.Capacitacion);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Capacitacion.Relevamiento.ID, tActivo = 6, mensaje = "Capacitacion guardada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public Relevamiento ObtenerOInsertarRelevamiento(RelevamientoModelo relevamientoModelo)
        {
            Relevamiento relevamiento;

            if (relevamientoModelo.Relevamiento.ID == 0)
            {
                relevamiento = new Relevamiento();
                relevamiento.Escuela = new Escuela() { ID = relevamientoModelo.Relevamiento.Escuela.ID };
                relevamiento.AtendidoPor = relevamientoModelo.Relevamiento.AtendidoPor;
                relevamiento.TieneADM = relevamientoModelo.Relevamiento.TieneADM;

                relevamientoComponente.GuardarRelevamiento(relevamiento);
            }
            else
            {
                relevamientoComponente.ActualizarFechaYModificadoPor(relevamientoModelo.Relevamiento.ID);

                relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };

            }

            return relevamiento;
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public List<Escuela> CargarEscuelasPorDistrito(int DistId)
        {

           return escuelaComponente.ObtenerEscuelasPorDistrito(DistId);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarEscuelasPorDistritoAsync(int DistId)
        {
            
            List<SelectListItem> listaEscuelas = new List<SelectListItem>(CargarEscuelasPorDistrito(DistId).OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));
            return Json(listaEscuelas, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarDatosEscuelaAsync(int EscId)
        {

            Escuela escuela = escuelaComponente.ObtenerEscuelaPorId(EscId);

            string director = "";
            string viceDirector = "";

            if(escuela != null)
            {
                if (escuela.Director != null)
                    director = escuela.Director;

                if (escuela.ViceDirector != null)
                    viceDirector = escuela.ViceDirector;
            }

            string historialComentarios = ConstruirHistorialComentarios(historialComentarioComponente.ObtenerHistorialComentarioPorEscuela(EscId));

            string historialSegPedagogico = ConstruirHistorialSegPedagogico(SeguimientoPedagogicoComponente.ObtenerSeguimientoPedagogicoPorEscuela(EscId));

            return Json(new { Director = director, ViceDirector = viceDirector, HistorialComentarios = historialComentarios, HistorialSegPedagogico = historialSegPedagogico }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerMaquinaAsync(int MaqId)
        {
            Maquina maq = maquinaComponente.ObtenerMaquinaPorId(MaqId);
            return Json(maq , JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerDispositivoAsync(int DisId)
        {
            Dispositivo dis = dispositivoComponente.ObtenerDispositivoPorId(DisId);

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion, TipoDispositivoId = dis.TipoDispositivo.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerSoftwareAsync(int SoftId)
        {
            Software soft = softwareComponente.ObtenerSoftwarePorId(SoftId);

            return Json(new { ID = soft.ID, Nombre = soft.Nombre, Descripcion = soft.Descripcion, TipoSoftwareId = soft.TipoSoftware.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerServicioAsync(int SerId)
        {
            Servicio ser = servicioComponente.ObtenerServicioPorId(SerId);

            return Json(new { ID = ser.ID, Compañia = ser.Compañia, EsPago = ser.EsPago, Descripcion = ser.Descripcion, TipoServicioId = ser.TipoServicio.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerCapacitacionAsync(int CapId)
        {
            Capacitacion cap = capacitacionComponente.ObtenerCapacitacionPorId(CapId);

            return Json(new { ID = cap.ID, Curso= cap.Curso, Descripcion = cap.Descripcion, Grado = cap.Grado }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerDispositivoRedAsync(int DisRedId)
        {
            DispositivoRed dis = dispositivoRedComponente.ObtenerDispositivoRedPorId(DisRedId);

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion, PuertosUtilizados = dis.PuertosUtilizados, PuertosTotales = dis.PuertosTotales, Protocolo = dis.Protocolo, TipoDispositivoRedId = dis.TipoDispositivoRed.ID }, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> ObtenerGrados()
        {
            List<SelectListItem> listaGrados = new List<SelectListItem>();

            listaGrados.Add(new SelectListItem() { Value = "1", Text = "1° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "2", Text = "2° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "3", Text = "3° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "4", Text = "4° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "5", Text = "5° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "6", Text = "6° Grado" });

            return listaGrados;
        }
    }
}
