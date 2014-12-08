using Escuelas.Comun;
using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.UI.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuelas.UI.Controllers
{
    public class InformeController : Controller
    {
        //
        // GET: /Informe/
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        HistorialComentarioComponente historialComentarioComponente = new HistorialComentarioComponente();
        SeguimientoPedagogicoComponente seguimientoPedagogicoComponente = new SeguimientoPedagogicoComponente();

        public ActionResult Index()
        {
            return View();
        }

        private void CargarDropDowns()
        {
            List<SelectListItem> listaInformes = new List<SelectListItem>();

            listaInformes.Add(new SelectListItem() { Text = "Equipamiento de Escuelas por Distrito", Value = Enums.TipoInforme.EquipamientoDeEscuelasPorDistrito.GetHashCode().ToString() });

            listaInformes.Add(new SelectListItem() { Text = "Listado de Escuelas por Distrito", Value = Enums.TipoInforme.ListadoEscuelas.GetHashCode().ToString() });

            listaInformes.Add(new SelectListItem() { Text = "Informe de Escuela Completo", Value = Enums.TipoInforme.InformeEscuelaCompleto.GetHashCode().ToString() });

            listaInformes.Add(new SelectListItem() { Text = "Informe de Escuela Capacitación y Herr. de Software", Value = Enums.TipoInforme.InformeEscuelaSoftyCapac.GetHashCode().ToString() });

            ViewBag.ListaInformes = listaInformes.OrderBy(i => i.Text).ToList();

            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            List<SelectListItem> listaRegiones = new List<SelectListItem>(listaDistritos.Select(d => new { region = d.Region }).Distinct().OrderBy(r => r.region).Select(item => new SelectListItem { Value = item.region.ToString(), Text = item.region.ToString() }));

            ViewBag.ListaRegiones = listaRegiones;

            List<SelectListItem> listaDistritosFitrados = new List<SelectListItem>(listaDistritos.Where(d => d.Region.ToString() == listaRegiones.First().Value).OrderBy(d => d.Nombre).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaDistritos = listaDistritosFitrados;

            ViewBag.ListaEscuelas = new List<SelectListItem>(escuelaComponente.ObtenerEscuelasPorDistrito(int.Parse(listaDistritosFitrados.First().Value)).OrderBy(e => e.Nombre).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));
        }
        [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult InformesIndex()
        {


            CargarDropDowns();
            

            return View(new InformeModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult InformesIndex(InformeModel modelo)
        {

            Relevamiento ultimo;

            switch (modelo.TipoInforme)
            {
                case (int)Enums.TipoInforme.EquipamientoDeEscuelasPorDistrito:
                    return new ActionAsPdf("ExportarEquipamientoEscuelas", new { distId = modelo.DistritoID }) { FileName = "EquipamientoEscuelas_" + modelo.DistritoID + ".pdf" };
                case (int)Enums.TipoInforme.InformeEscuelaCompleto:

                    ultimo = relevamientoComponente.ObtenerUltimoRelevamiento(modelo.EscuelaID);
                     if (ultimo != null)
                    {
                    return new ActionAsPdf("ExportarRelevamiento", new { relevamientoId = ultimo.ID }) { FileName = "RelevamientoCompleto_" + ultimo.ID + ".pdf" };
                    }
                     else
                     {
                         this.ModelState.AddModelError("", "La Escuela seleccionada no ha sido relevada");
                         CargarDropDowns();
                         return View(modelo);
                     }
                case (int)Enums.TipoInforme.InformeEscuelaSoftyCapac:

                    ultimo = relevamientoComponente.ObtenerUltimoRelevamiento(modelo.EscuelaID);
                    if (ultimo != null)
                    {
                        return new ActionAsPdf("ExportarHerramientasSoft", new { relevamientoId = ultimo.ID }) { FileName = "RelevamientoHerramientasSoft_" + ultimo.ID + ".pdf" };
                    }
                    else
                    {
                        this.ModelState.AddModelError("", "La Escuela seleccionada no ha sido relevada");
                        CargarDropDowns();
                        return View(modelo);
                    }
                    

                default:
                    return new ActionAsPdf("ExportarListadoEscuelas", new { distId = modelo.DistritoID }) { FileName = "ListadoEscuelas_" + modelo.DistritoID + ".pdf" };
            }

        }

        public ActionResult CargarDistritosPorRegionAsync(int Region)
        {
            List<SelectListItem> listaDistritos = new List<SelectListItem>(distritoComponente.ObtenerDistritos().Where(d => d.Region == Region).OrderBy(d => d.Nombre).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            DistritoEscuelaModel modelo = new DistritoEscuelaModel();

            modelo.listaDistritos = listaDistritos;

            modelo.listaEscuelas = new List<SelectListItem>(CargarEscuelasPorDistrito(int.Parse(listaDistritos.First().Value)).OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            return Json(modelo, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarEscuelasPorDistritoAsync(int DistId)
        {

            List<SelectListItem> listaEscuelas = new List<SelectListItem>(CargarEscuelasPorDistrito(DistId).OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));
            return Json(listaEscuelas, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public List<Escuela> CargarEscuelasPorDistrito(int DistId)
        {

            return escuelaComponente.ObtenerEscuelasPorDistrito(DistId);
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportarRelevamiento(int relevamientoId)
        {
            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();
            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            relevamientoModelo.HistorialComentarios = historialComentarioComponente.ConstruirHistorialComentarios(historialComentarioComponente.ObtenerHistorialComentarioPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));
            relevamientoModelo.HistorialSegPedagogico = seguimientoPedagogicoComponente.ConstruirHistorialSegPedagogico(seguimientoPedagogicoComponente.ObtenerSeguimientoPedagogicoPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));
            return View(relevamientoModelo);
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportarHerramientasSoft(int relevamientoId)
        {
            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();
            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(relevamientoId);
            return View(relevamientoModelo);
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportarListadoEscuelas(int distId)
        {
            List<Escuela> listaEscuelas = escuelaComponente.ObtenerEscuelasPorDistrito(distId).OrderBy(d => d.Numero).ToList();
            return View(listaEscuelas);
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportarEquipamientoEscuelas(int distId)
        {
            List<ReporteEquipamientoEscuelas> listaEscuelas = escuelaComponente.ObtenerReporteEquipamientoEscuela(distId).OrderBy(d => d.EscuelaNumero).ToList();
            return View(listaEscuelas);
        }
        
    }
}
