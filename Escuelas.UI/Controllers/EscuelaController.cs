using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Controllers
{
    public class EscuelaController : Controller
    {
        //
        // GET: /Escuela/
        EscuelaComponente escuelaComponente = new EscuelaComponente(); 
        DistritoComponente distritoComponente = new DistritoComponente();

        public ActionResult EscuelaIndex()
        {
            return View(escuelaComponente.ObtenerEscuelas());
        }
        public ActionResult EditarEscuela(int escuelaId)
        {
            ViewBag.ListaDistritos = new List<SelectListItem>(distritoComponente.ObtenerDistritos().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            Escuela escuela; 

            if(escuelaId > 0)
            {
                escuela = escuelaComponente.ObtenerEscuelaPorId(escuelaId);
            }
            else
            {
                escuela = new Escuela();
                escuela.Distrito = new Distrito();
                escuela.TipoEstablecimiento = new CategoriaValor();
            }
            return View(escuela);
        }
        [HttpPost]
        public ActionResult EditarEscuela(Escuela escuela)
        {
          
            escuelaComponente.GuardarEscuela(escuela);

            return RedirectToAction("EscuelaIndex");
        }
    }
}
