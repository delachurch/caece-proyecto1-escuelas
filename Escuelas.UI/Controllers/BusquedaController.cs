using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Controllers
{
    public class BusquedaController : Controller
    {

        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        DistritoComponente distritoComponente = new DistritoComponente();

        //
        // GET: /Busqueda/
        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult BusquedaIndex(string distId, string escId)
        {
            RelevamientoBusqueda relevamientoBusqueda = new RelevamientoBusqueda();

            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            List<Escuela> listaEscuelas;

            if (!string.IsNullOrEmpty(distId))
            {
                relevamientoBusqueda.DistritoId = distId;
                listaEscuelas = CargarEscuelasPorDistrito(int.Parse(relevamientoBusqueda.DistritoId));
            }
            else
            {
                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);
            }



            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            if (!string.IsNullOrEmpty(escId))
            {
                relevamientoBusqueda.EscuelaId = escId;
            }

            // relevamientoBusqueda.Relevamientos = relevamientoComponente.ObtenerRelevamientosPorEscuela(int.Parse(relevamientoBusqueda.EscuelaId));

            return View(relevamientoBusqueda);

        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        [HttpPost]
        public ActionResult BusquedaIndex(RelevamientoBusqueda relevamientoBusqueda)
        {
            return RedirectToAction("RelevamientoIndex", new { distId = relevamientoBusqueda.DistritoId, escId = relevamientoBusqueda.EscuelaId });
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

    }
}
