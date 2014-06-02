using Escuelas.Comun;
using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Newtonsoft.Json;

namespace relevamientos.UI.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class RelevamientoController : Controller
    {
        //
        // GET: /Relevamiento/
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        MaquinaComponente maquinaComponente = new MaquinaComponente();
        DispositivoComponente dispositivoComponente = new DispositivoComponente();
        DispositivoRedComponente dispositivoRedComponente = new DispositivoRedComponente();
        ServicioComponente servicioComponente = new ServicioComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        public ActionResult RelevamientoIndex(int? distId, int? escId)
        {
            RelevamientoBusqueda relevamientoBusqueda = new RelevamientoBusqueda();
                    
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            if (distId.Value > 0)
            {
                relevamientoBusqueda.DistritoId = distId.Value;
            }
            else
            {
                relevamientoBusqueda.DistritoId = listaDistritos.First().ID;
            }

            List<Escuela> listaEscuelas = CargarEscuelasPorDistrito(relevamientoBusqueda.DistritoId);

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            if (escId.Value > 0)
            {
                relevamientoBusqueda.EscuelaId = escId.Value;
            }
            else
            {
                relevamientoBusqueda.EscuelaId = listaEscuelas.First().ID;
            }

            relevamientoBusqueda.Relevamientos = relevamientoComponente.ObtenerRelevamientosPorEscuela(relevamientoBusqueda.EscuelaId);

            return View(relevamientoBusqueda);

        }
        [HttpPost]
        public ActionResult RelevamientoIndex(RelevamientoBusqueda relevamientoBusqueda)
        {
            return RedirectToAction("RelevamientoIndex", new { distId = relevamientoBusqueda.DistritoId, escId = relevamientoBusqueda.EscuelaId });
        }

        public ActionResult ExportPDF(int relevamientoId)
        {
            return new ActionAsPdf("ExportarRelevamiento", new { relevamientoId = relevamientoId }) {FileName = "Relevamiento.pdf" };
        }


        public ActionResult ExportarRelevamiento(int relevamientoId)
        {

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();
            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            return View(relevamientoModelo);
        }
        public ActionResult CopiarUltimoRelevamiento(int escuelaId)
        {

            Relevamiento relevamiento = relevamientoComponente.ObtenerUltimoRelevamiento(escuelaId);

            relevamientoComponente.CopiarRelevamiento(relevamiento);


            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamiento.ID, tActivo = 0, mensaje = "Relevamiento copiado exitosamente" });
        }
        public ActionResult EditarRelevamiento(int relevamientoId, int tActivo, string mensaje)
        {  
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivos = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivo.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivosRed = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivoRed.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoServicios = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoServicio.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.Mensaje = mensaje;

            List<Escuela> listaEscuelas;
          
            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();

            relevamientoModelo.Maquina = new Maquina();

            relevamientoModelo.Dispositivo = new Dispositivo();

            relevamientoModelo.Dispositivo.TipoDispositivo = new CategoriaValor();

            relevamientoModelo.Servicio= new Servicio();

            relevamientoModelo.Servicio.TipoServicio = new CategoriaValor();

            relevamientoModelo.DispositivoRed = new DispositivoRed();

            relevamientoModelo.DispositivoRed.TipoDispositivoRed = new CategoriaValor();

            relevamientoModelo.TabActivo = tActivo;

            if (relevamientoId > 0)
            {
                
                relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);

                listaEscuelas = CargarEscuelasPorDistrito(relevamientoModelo.Relevamiento.Escuela.Distrito.ID);

                ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            }
            else
            {
                relevamientoModelo.Relevamiento = new Relevamiento();

                int escuelaId = 0;

                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);

                if (listaEscuelas.Count() > 0)
                    escuelaId = listaEscuelas.First().ID;

                relevamientoModelo.Relevamiento.Escuela = new Escuela() { ID =  escuelaId } ;
                relevamientoModelo.Relevamiento.Escuela.Distrito = new Distrito();
            }

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));
            
            return View(relevamientoModelo);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarRelevamiento(RelevamientoModelo relevamientoModelo)
        {
           


            relevamientoComponente.GuardarRelevamiento(relevamientoModelo.Relevamiento);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Relevamiento.ID, tActivo = 0, mensaje= "Relevamiento guardado" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarMaquina(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Maquina.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            maquinaComponente.GuardarMaquina(relevamientoModelo.Maquina);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Maquina.Relevamiento.ID, tActivo = 1, mensaje = "Maquina guardada" });
        }

        public ActionResult BorrarMaquina(int maqId,int relId)
        {

            maquinaComponente.BorrarMaquina(maqId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 1,  mensaje = "Maquina borrada" });
        }

        public ActionResult BorrarDispositivo(int disId, int relId)
        {

            dispositivoComponente.BorrarDispositivo(disId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 3, mensaje = "Dispositivo borrado" });
        }

        public ActionResult BorrarDispositivoRed(int disRedId, int relId)
        {

            dispositivoRedComponente.BorrarDispositivoRed(disRedId);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 2, mensaje = "Dispositivo de Red borrado" });
        }

        public ActionResult BorrarServicio(int serId, int relId)
        {

           servicioComponente.BorrarServicio(serId);

           return RedirectToAction("EditarRelevamiento", new { relevamientoId = relId, tActivo = 4, mensaje = "Servicio borrado" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarDispositivo(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Dispositivo.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoComponente.GuardarDispositivo(relevamientoModelo.Dispositivo);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Dispositivo.Relevamiento.ID, tActivo = 3, mensaje = "Dispositivo guardado" });
        }

        [HttpPost]
        public ActionResult EditarDispositivoRed(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.DispositivoRed.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoRedComponente.GuardarDispositivoRed(relevamientoModelo.DispositivoRed);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.DispositivoRed.Relevamiento.ID, tActivo = 2, mensaje = "Dispositivo de Red guardado" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarServicio(RelevamientoModelo relevamientoModelo)
        {


            relevamientoModelo.Servicio.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            servicioComponente.GuardarServicio(relevamientoModelo.Servicio);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Servicio.Relevamiento.ID, tActivo = 4, mensaje = "Servicio guardado" });
        }
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
                relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };
            }

            return relevamiento;
        }
        public List<Escuela> CargarEscuelasPorDistrito(int DistId)
        {

           return escuelaComponente.ObtenerEscuelasPorDistrito(DistId);
        }
        public ActionResult CargarEscuelasPorDistritoAsync(int DistId)
        {
            List<SelectListItem> listaEscuelas =  new List<SelectListItem>(CargarEscuelasPorDistrito(DistId).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));
            return Json(listaEscuelas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerMaquinaAsync(int MaqId)
        {
            Maquina maq = maquinaComponente.ObtenerMaquinaPorId(MaqId);
            return Json(maq , JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerDispositivoAsync(int DisId)
        {
            Dispositivo dis = dispositivoComponente.ObtenerDispositivoPorId(DisId);

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion, TipoDispositivoId = dis.TipoDispositivo.ID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerServicioAsync(int SerId)
        {
            Servicio ser = servicioComponente.ObtenerServicioPorId(SerId);

            return Json(new { ID = ser.ID, Compañia = ser.Compañia, EsPago = ser.EsPago, Descripcion = ser.Descripcion, TipoServicioId = ser.TipoServicio.ID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerDispositivoRedAsync(int DisRedId)
        {
            DispositivoRed dis = dispositivoRedComponente.ObtenerDispositivoRedPorId(DisRedId);

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion,PuertosUtilizados = dis.PuertosUtilizados, Protocolo = dis.Protocolo, TipoDispositivoRedId = dis.TipoDispositivoRed.ID }, JsonRequestBehavior.AllowGet);
        }
    }
}
