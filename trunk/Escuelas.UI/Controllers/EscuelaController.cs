using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Escuelas.UI.Filters;
using Escuelas.UI.Models;

namespace Escuelas.UI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class EscuelaController : Controller
    {
        //
        // GET: /Escuela/
        EscuelaComponente escuelaComponente = new EscuelaComponente(); 
        DistritoComponente distritoComponente = new DistritoComponente();

        [Authorize(Roles = "Admin,ReadOnly")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EscuelaIndex()
        {
            return View(escuelaComponente.ObtenerEscuelas());
        }

        [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarEscuela(Escuela escuela)
        {
          
            escuelaComponente.GuardarEscuela(escuela);

            return RedirectToAction("EscuelaIndex");
        }

        public ActionResult BorrarEscuela(int escuelaId)
        {

            escuelaComponente.BorrarEscuela(escuelaId);

            return RedirectToAction("EscuelaIndex");
        }

    }
}
