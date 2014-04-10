using Escuelas.NegocioComponentes;
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

        public ActionResult EscuelaIndex()
        {
            return View(escuelaComponente.ObtenerEscuelas());
        }

    }
}
