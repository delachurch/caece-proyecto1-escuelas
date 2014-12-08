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
using Escuelas.Comun;
using Escuelas.Seguridad;
using Rotativa;

namespace Escuelas.UI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class EscuelaController : Controller
    {
        //
        // GET: /Escuela/
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente(); 
        DistritoComponente distritoComponente = new DistritoComponente();

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EscuelaIndex()
        {
            EscuelaModel escModel;
            List<EscuelaModel> listaEscuelaModel = new List<EscuelaModel>();

            foreach (Escuela esc in escuelaComponente.ObtenerEscuelas())
            {
                escModel = new EscuelaModel();
                escModel.Escuela = esc;
                escModel.encId = Encriptacion.EncriptarID(esc.ID);
                listaEscuelaModel.Add(escModel);
            }
            return View(listaEscuelaModel);
        }

        [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarEscuela(string escuelaId)
        {
            ViewBag.ListaDistritos = new List<SelectListItem>(distritoComponente.ObtenerDistritos().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            Escuela escuela;

            if(!string.IsNullOrEmpty(escuelaId))
            {
                escuela = escuelaComponente.ObtenerEscuelaPorId(Encriptacion.DesencriptarID(escuelaId));
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

        [Authorize(Roles = "Admin")]
        public ActionResult BorrarEscuela(string escuelaId)
        {

            if (escuelaId != null && escuelaId != "")
            {
                escuelaComponente.BorrarEscuela(Encriptacion.DesencriptarID(escuelaId));
            }

            return RedirectToAction("EscuelaIndex");
        }

        


    }
}
