using Escuelas.Comun;
using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.Seguridad;
using Escuelas.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Controllers
{
    public class DistritoController : Controller
    {
        //
        // GET: /Distrito/
        private DistritoComponente distritoComponente = new DistritoComponente();
        [Authorize(Roles = "Admin")]
        public ActionResult DistritoIndex()
        {
            return View(distritoComponente.ObtenerDistritos());
        }

        [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarDistrito(int distritoId)
        {

            Distrito distrito;

            if (distritoId > 0)
            {
                distrito = distritoComponente.ObtenerDistritoPorId(distritoId);
            }
            else
            {
                distrito = new Distrito();
            }
            return View(distrito);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarDistrito(Distrito distrito)
        {

            distritoComponente.GuardarDistrito(distrito);

            return RedirectToAction("DistritoIndex");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BorrarDistrito(int distritoId)
        {

            distritoComponente.BorrarDistrito(distritoId);

            return RedirectToAction("DistritoIndex");
        }
    }
}
