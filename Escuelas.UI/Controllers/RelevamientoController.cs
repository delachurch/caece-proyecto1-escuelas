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
        ServicioComponente servicioComponente = new ServicioComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        public ActionResult RelevamientoIndex(int? distId, int? escId)
        {
       
                    
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));
            
            List<Escuela> listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            int escuelaId;

            if (escId.Value > 0)
                escuelaId = escId.Value ;
            else
                escuelaId = listaEscuelas.First().ID;

            
            RelevamientoBusqueda relevamientoBusqueda = new RelevamientoBusqueda();
            

            relevamientoBusqueda.EscuelaId = escId.Value;

            relevamientoBusqueda.DistritoId = distId.Value;

            relevamientoBusqueda.Relevamientos = relevamientoComponente.ObtenerRelevamientosPorEscuela(escuelaId);

            return View(relevamientoBusqueda);

        }
        [HttpPost]
        public ActionResult RelevamientoIndex(RelevamientoBusqueda relevamientoBusqueda)
        {
            return RedirectToAction("RelevamientoIndex", new { distId = relevamientoBusqueda.DistritoId, escId = relevamientoBusqueda.EscuelaId });
        }

        public ActionResult ExportPDF(int relevamientoId)
        {
            return new ActionAsPdf("ExpotarRelevamiento", new { relevamientoId = relevamientoId }) {FileName = "Relevamiento.pdf" };
        }


        public ActionResult ExportarRelevamiento(int relevamientoId)
        {

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();
            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            return View(relevamientoModelo.Relevamiento);
        }

        public ActionResult EditarRelevamiento(int relevamientoId, int tActivo)
        {   
            Relevamiento relevamiento;

            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivos = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivo.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoServicios = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoServicio.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            List<Escuela> listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();

            relevamientoModelo.Maquina = new Maquina();

            relevamientoModelo.Dispositivo = new Dispositivo();

            relevamientoModelo.Dispositivo.TipoDispositivo = new CategoriaValor();

            relevamientoModelo.Servicio= new Servicio();

            relevamientoModelo.Servicio.TipoServicio = new CategoriaValor();

            relevamientoModelo.TabActivo = tActivo;

            if (relevamientoId > 0)
            {
                
                relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            }
            else
            {
                relevamientoModelo.Relevamiento = new Relevamiento();
                relevamientoModelo.Relevamiento.Escuela = new Escuela();
                relevamientoModelo.Relevamiento.Escuela.Distrito = new Distrito();
            }

            return View(relevamientoModelo);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarRelevamiento(RelevamientoModelo relevamientoModelo)
        {
           


            relevamientoComponente.GuardarRelevamiento(relevamientoModelo.Relevamiento);

            return RedirectToAction("RelevamientoIndex");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarMaquina(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Maquina.Relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };


            maquinaComponente.GuardarMaquina(relevamientoModelo.Maquina);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Relevamiento.ID,tActivo = 1 });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarDispositivo(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Dispositivo.Relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };

            dispositivoComponente.GuardarDispositivo(relevamientoModelo.Dispositivo);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Relevamiento.ID, tActivo = 2 });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarServicio(RelevamientoModelo relevamientoModelo)
        {
            relevamientoModelo.Servicio.Relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };

            servicioComponente.GuardarServicio(relevamientoModelo.Servicio);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = relevamientoModelo.Relevamiento.ID, tActivo = 3 });
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
    }
}
