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

namespace relevamientos.UI.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class RelevamientoController : Controller
    {
        //
        // GET: /Relevamiento/
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        public ActionResult RelevamientoIndex()
        {

            return View(relevamientoComponente.ObtenerRelevamientos());

        }

        public ActionResult EditarRelevamiento(int relevamientoId)
        {   
            Relevamiento relevamiento;

            List<Distrito> ListaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(ListaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivos = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValor(Enums.Categoria.TipoDispositivo.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaEscuelas = CargarEscuelasPorDistrito(ListaDistritos.First().ID);

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();

            relevamientoModelo.Maquina = new Maquina();

            relevamientoModelo.Dispositivo = new Dispositivo();

            relevamientoModelo.Dispositivo.TipoDispositivo = new CategoriaValor(); 
            
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
            relevamientoModelo.Relevamiento.Escuela.Distrito = null;


            relevamientoComponente.GuardarRelevamiento(relevamientoModelo.Relevamiento);

            return RedirectToAction("RelevamientoIndex");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarMaquina(Relevamiento relevamiento)
        {
            string lala = "lala";

            return RedirectToAction("RelevamientoIndex");
        }

        public List<SelectListItem> CargarEscuelasPorDistrito(int DistId)
        {
            List<Escuela> listaEscuelas = escuelaComponente.ObtenerEscuelasPorDistrito(DistId);
            return new List<SelectListItem>(listaEscuelas.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));
        }
        public ActionResult CargarEscuelasPorDistritoAsync(int DistId)
        {
            return Json(CargarEscuelasPorDistrito(DistId), JsonRequestBehavior.AllowGet);
        }
    }
}
