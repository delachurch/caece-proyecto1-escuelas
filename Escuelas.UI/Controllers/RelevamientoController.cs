using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
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

        public ActionResult RelevamientoIndex()
        {

            return View(relevamientoComponente.ObtenerRelevamientos());

        }

        public ActionResult EditarRelevamiento(int relevamientoId)
        {   
            Relevamiento relevamiento;

            List<Distrito> ListaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(ListaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaEscuelas = CargarEscuelasPorDistrito(ListaDistritos.First().ID);

            if (relevamientoId > 0)
            {
                relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            }
            else
            {
                relevamiento = new Relevamiento();
                relevamiento.Escuela = new Escuela();
                relevamiento.Escuela.Distrito = new Distrito();
            }

            return View(relevamiento);

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
