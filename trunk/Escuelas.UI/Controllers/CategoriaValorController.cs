using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Controllers
{
    public class CategoriaValorController : Controller
    {
        //
        // GET: /CategoriaValor/
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        public ActionResult OpcionesIndex()
        {

            return View(categoriaValorComponente.ObtenerCategoriasValor());
        }

        [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarOpcion(int opcionId)
        {
            ViewBag.ListaCategorias = new List<SelectListItem>(categoriaValorComponente.ObtenerCategorias().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            CategoriaValor categoriaValor;

            if (opcionId > 0)
            {
                categoriaValor = categoriaValorComponente.ObtenerCategoriaValorPorId(opcionId);
            }
            else
            {
                categoriaValor = new CategoriaValor();

                categoriaValor.Categoria = new Categoria();
            }
            return View(categoriaValor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarOpcion(CategoriaValor opcion)
        {

            categoriaValorComponente.GuardarCategoriaValor(opcion);

            return RedirectToAction("OpcionesIndex");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BorrarOpcion(int opcionId)
        {

            categoriaValorComponente.BorrarCategoriaValor(opcionId);

            return RedirectToAction("OpcionesIndex");
        }

    }
}
