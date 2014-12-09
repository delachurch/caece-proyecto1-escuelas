using Escuelas.Comun;
using Escuelas.NegocioComponentes;
using Escuelas.NegocioEntidades;
using Escuelas.Seguridad;
using Escuelas.UI.Models;
using Escuelas.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace relevamientos.UI.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class RelevamientoController : Controller
    {
        //
        // GET: /Relevamiento/
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        HistorialComentarioComponente historialComentarioComponente = new HistorialComentarioComponente();
        SeguimientoPedagogicoComponente seguimientoPedagogicoComponente = new SeguimientoPedagogicoComponente();
        UsuarioComponente usuarioComponente = new UsuarioComponente();
        MaquinaComponente maquinaComponente = new MaquinaComponente();
        DispositivoComponente dispositivoComponente = new DispositivoComponente();
        SoftwareComponente softwareComponente = new SoftwareComponente();
        DispositivoRedComponente dispositivoRedComponente = new DispositivoRedComponente();
        ServicioComponente servicioComponente = new ServicioComponente();
        CapacitacionComponente capacitacionComponente = new CapacitacionComponente();
        ImagenComponente imagenComponente = new ImagenComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        CategoriaValorComponente categoriaValorComponente = new CategoriaValorComponente();

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult RelevamientoIndex(string distId, string escId)
        {
            RelevamientoBusqueda relevamientoBusqueda = new RelevamientoBusqueda();

            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = Encriptacion.EncriptarID(item.ID), Text = item.Nombre }));

            List<Escuela> listaEscuelas;

            if (!string.IsNullOrEmpty(distId))
            {
                relevamientoBusqueda.DistritoId = distId;
                listaEscuelas = CargarEscuelasPorDistrito(Encriptacion.DesencriptarID(distId));
            }
            else
            {
                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);
            }



            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = Encriptacion.EncriptarID(item.ID), Text = item.Numero + " - " + item.Nombre }));

            relevamientoBusqueda.Relevamientos = new List<RelevamientoEncID>();

            List<Relevamiento> resultadoBusqueda;

            if (!string.IsNullOrEmpty(escId))
            {
                resultadoBusqueda = relevamientoComponente.ObtenerRelevamientosPorEscuela(Encriptacion.DesencriptarID(escId));
            }
            else
            {
                if (!string.IsNullOrEmpty(distId))
                {
                    resultadoBusqueda = relevamientoComponente.ObtenerRelevamientosPorDistrito(Encriptacion.DesencriptarID(distId));
                }
                else
                {
                    resultadoBusqueda = relevamientoComponente.ObtenerRelevamientosPorEscuela(0);
                }
            }

            relevamientoBusqueda.EscuelaId = escId;
            RelevamientoEncID relenc;

            foreach (Relevamiento rel in resultadoBusqueda)
            {
                relenc = new RelevamientoEncID();
                relenc.Relevamiento = rel;
                relenc.EncId = Encriptacion.EncriptarID(rel.ID);
                    
                relevamientoBusqueda.Relevamientos.Add(relenc);
            }

            
            

            return View(relevamientoBusqueda);

        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        [HttpPost]
        public ActionResult RelevamientoIndex(RelevamientoBusqueda relevamientoBusqueda)
        {
            return RedirectToAction("RelevamientoIndex", new { distId = relevamientoBusqueda.DistritoId, escId = relevamientoBusqueda.EscuelaId });
        }

        [Authorize(Roles = "Admin,ReadOnly,Colaborador")]
        public ActionResult ExportPDF(string relevamientoId)
        {
            int relId = Encriptacion.DesencriptarID(relevamientoId);
            return new ActionAsPdf("ExportarRelevamiento", new { relevamientoId = relId }) { FileName = "RelevamientoCompleto_" + relId + ".pdf" };
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


        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CopiarUltimoRelevamiento(int escuelaId)
        {

            Relevamiento relevamiento = relevamientoComponente.ObtenerUltimoRelevamiento(escuelaId);

            relevamientoComponente.CopiarRelevamiento(relevamiento);


            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamiento.ID), tActivo = 0, mensaje = "Relevamiento copiado exitosamente" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult EditarRelevamiento(string relevamientoId, int tActivo, string mensaje)
        {
            List<Distrito> listaDistritos = distritoComponente.ObtenerDistritos();

            ViewBag.ListaDistritos = new List<SelectListItem>(listaDistritos.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivos = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValorPorCategoria(Enums.Categoria.TipoDispositivo.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoSoftwares = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValorPorCategoria(Enums.Categoria.TipoSoftware.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaPlataformaSoftwares = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValorPorCategoria(Enums.Categoria.PlataformaSoftware.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoDispositivosRed = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValorPorCategoria(Enums.Categoria.TipoDispositivoRed.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaTipoServicios = new List<SelectListItem>(categoriaValorComponente.ObtenerCategoriasValorPorCategoria(Enums.Categoria.TipoServicio.GetHashCode()).Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Nombre }));

            ViewBag.ListaGrados = ObtenerGrados();

            ViewBag.Mensaje = mensaje;

            List<Escuela> listaEscuelas;

            RelevamientoModelo relevamientoModelo = new RelevamientoModelo();

            relevamientoModelo.Maquina = new Maquina();

            relevamientoModelo.Dispositivo = new Dispositivo();

            relevamientoModelo.Dispositivo.TipoDispositivo = new CategoriaValor();

            relevamientoModelo.Software = new Software();

            relevamientoModelo.Software.TipoSoftware = new CategoriaValor();

            relevamientoModelo.Software.PlataformaSoftware = new CategoriaValor();

            relevamientoModelo.Capacitacion = new Capacitacion();

            relevamientoModelo.Imagen = new Imagen();

            relevamientoModelo.Servicio = new Servicio();

            relevamientoModelo.Servicio.TipoServicio = new CategoriaValor();

            relevamientoModelo.DispositivoRed = new DispositivoRed();

            relevamientoModelo.DispositivoRed.TipoDispositivoRed = new CategoriaValor();

            relevamientoModelo.TabActivo = tActivo;

            if (!string.IsNullOrEmpty(relevamientoId))
            {

                CargarModelo(relevamientoModelo, relevamientoId);

                listaEscuelas = CargarEscuelasPorDistrito(relevamientoModelo.Relevamiento.Escuela.Distrito.ID);

                ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            }
            else
            {
                relevamientoModelo.Relevamiento = new Relevamiento();

                int escuelaId = 0;
                string director = "";
                string viceDirector = "";

                listaEscuelas = CargarEscuelasPorDistrito(listaDistritos.First().ID);

                if (listaEscuelas.Count() > 0)
                {
                    escuelaId = listaEscuelas.First().ID;
                    director = listaEscuelas.First().Director;
                    viceDirector = listaEscuelas.First().ViceDirector;
                }


                relevamientoModelo.Relevamiento.Escuela = new Escuela() { ID = escuelaId };
                relevamientoModelo.Relevamiento.Escuela.Distrito = new Distrito();
                relevamientoModelo.Relevamiento.Escuela.Director = director;
                relevamientoModelo.Relevamiento.Escuela.ViceDirector = viceDirector;
            }

            ViewBag.ListaEscuelas = new List<SelectListItem>(listaEscuelas.OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));

            relevamientoModelo.HistorialComentarios = historialComentarioComponente.ConstruirHistorialComentarios(historialComentarioComponente.ObtenerHistorialComentarioPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));

            relevamientoModelo.HistorialSegPedagogico = seguimientoPedagogicoComponente.ConstruirHistorialSegPedagogico(seguimientoPedagogicoComponente.ObtenerSeguimientoPedagogicoPorEscuela(relevamientoModelo.Relevamiento.Escuela.ID));

            return View(relevamientoModelo);

        }
        private void CargarModelo(RelevamientoModelo relevamientoModelo, string relevamientoId)
        {

            relevamientoModelo.Relevamiento = relevamientoComponente.ObtenerRelevamientoPorId(Encriptacion.DesencriptarID(relevamientoId));

            relevamientoModelo.ListaMaquinas = new List<MaquinaModel>();
            MaquinaModel maqModel;
            foreach (Maquina maq in relevamientoModelo.Relevamiento.Maquinas)
            {
                maqModel = new MaquinaModel();
                maqModel.Maquina = maq;
                maqModel.EncId = Encriptacion.EncriptarID(maq.ID);
                relevamientoModelo.ListaMaquinas.Add(maqModel);
            }

            relevamientoModelo.ListaDispositivosRed = new List<DispositivoRedModel>();
            DispositivoRedModel disredModel;
            foreach (DispositivoRed disred in relevamientoModelo.Relevamiento.DispositivosRed)
            {
                disredModel = new DispositivoRedModel();
                disredModel.DispositivoRed = disred;
                disredModel.EncId = Encriptacion.EncriptarID(disred.ID);
                relevamientoModelo.ListaDispositivosRed.Add(disredModel);
            }

            relevamientoModelo.ListaDispositivos = new List<DispositivoModel>();
            DispositivoModel disModel;
            foreach (Dispositivo dis in relevamientoModelo.Relevamiento.Dispositivos)
            {
                disModel = new DispositivoModel();
                disModel.Dispositivo = dis;
                disModel.EncId = Encriptacion.EncriptarID(dis.ID);
                relevamientoModelo.ListaDispositivos.Add(disModel);
            }

            relevamientoModelo.ListaServicios = new List<ServicioModel>();
            ServicioModel serModel;
            foreach (Servicio ser in relevamientoModelo.Relevamiento.Servicios)
            {
                serModel = new ServicioModel();
                serModel.Servicio = ser;
                serModel.EncId = Encriptacion.EncriptarID(ser.ID);
                relevamientoModelo.ListaServicios.Add(serModel);
            }

            relevamientoModelo.ListaCapacitaciones = new List<CapacitacionModel>();
            CapacitacionModel capModel;
            foreach (Capacitacion cap in relevamientoModelo.Relevamiento.Capacitaciones)
            {
                capModel = new CapacitacionModel();
                capModel.Capacitacion = cap;
                capModel.EncId = Encriptacion.EncriptarID(cap.ID);
                relevamientoModelo.ListaCapacitaciones.Add(capModel);
            }

            relevamientoModelo.ListaSoftwares = new List<SoftwareModel>();
            SoftwareModel sofModel;
            foreach (Software sof in relevamientoModelo.Relevamiento.Softwares)
            {
                sofModel = new SoftwareModel();
                sofModel.Software = sof;
                sofModel.EncId = Encriptacion.EncriptarID(sof.ID);
                relevamientoModelo.ListaSoftwares.Add(sofModel);
            }

            relevamientoModelo.ListaImagenes = new List<ImagenModel>();
            ImagenModel imaModel;
            foreach (Imagen ima in relevamientoModelo.Relevamiento.Imagenes)
            {
                imaModel = new ImagenModel();
                imaModel.Imagen = ima;
                imaModel.EncId = Encriptacion.EncriptarID(ima.ID);
                relevamientoModelo.ListaImagenes.Add(imaModel);
            }

        }
        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarRelevamiento(RelevamientoModelo relevamientoModelo)
        {

            relevamientoComponente.GuardarRelevamiento(relevamientoModelo.Relevamiento);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Relevamiento.ID), tActivo = 0, mensaje = "Relevamiento guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarMaquina(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Maquina.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            maquinaComponente.GuardarMaquina(relevamientoModelo.Maquina);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Maquina.Relevamiento.ID), tActivo = 1, mensaje = "Maquina guardada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarMaquina(string maqId, int relId)
        {

            maquinaComponente.BorrarMaquina(Encriptacion.DesencriptarID(maqId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 1, mensaje = "Maquina borrada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarDispositivo(string disId, int relId)
        {

            dispositivoComponente.BorrarDispositivo(Encriptacion.DesencriptarID(disId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 3, mensaje = "Dispositivo borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarSoftware(string softId, int relId)
        {

            softwareComponente.BorrarSoftware(Encriptacion.DesencriptarID(softId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 5, mensaje = "Software borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarDispositivoRed(string disRedId, int relId)
        {

            dispositivoRedComponente.BorrarDispositivoRed(Encriptacion.DesencriptarID(disRedId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 2, mensaje = "Dispositivo de Red borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarServicio(string serId, int relId)
        {

            servicioComponente.BorrarServicio(Encriptacion.DesencriptarID(serId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 4, mensaje = "Servicio borrado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarCapacitacion(string capId, int relId)
        {

            capacitacionComponente.BorrarCapacitacion(Encriptacion.DesencriptarID(capId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 6, mensaje = "Capacitacion borrada" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult BorrarImagen(string imgId, int relId)
        {

            imagenComponente.BorrarImagen(Encriptacion.DesencriptarID(imgId));

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relId), tActivo = 9, mensaje = "Imagen borrada" });
        }


        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarDispositivo(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Dispositivo.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoComponente.GuardarDispositivo(relevamientoModelo.Dispositivo);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Dispositivo.Relevamiento.ID), tActivo = 3, mensaje = "Dispositivo guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarSoftware(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.Software.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            softwareComponente.GuardarSoftware(relevamientoModelo.Software);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Software.Relevamiento.ID), tActivo = 5, mensaje = "Software guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarDispositivoRed(RelevamientoModelo relevamientoModelo)
        {

            relevamientoModelo.DispositivoRed.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            dispositivoRedComponente.GuardarDispositivoRed(relevamientoModelo.DispositivoRed);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.DispositivoRed.Relevamiento.ID), tActivo = 2, mensaje = "Dispositivo de Red guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarSegPedagogico(RelevamientoModelo relevamientoModelo)
        {

            Relevamiento relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            SeguimientoPedagogico SeguimientoPedagogico = new SeguimientoPedagogico();

            SeguimientoPedagogico.Escuela = new Escuela { ID = relevamientoModelo.Relevamiento.Escuela.ID };
            SeguimientoPedagogico.UserProfile = new Escuelas.NegocioEntidades.UserProfile { UserId = UsuarioActual.ObtenerUsuarioActual().UserId };
            SeguimientoPedagogico.FechaAlta = DateTime.Now;
            SeguimientoPedagogico.Comentarios = relevamientoModelo.SeguimientoPedagogico;

            seguimientoPedagogicoComponente.GuardarSegPedagogico(SeguimientoPedagogico);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamiento.ID), tActivo = 8, mensaje = "Comentario Guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarComentarios(RelevamientoModelo relevamientoModelo)
        {

            Relevamiento relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            HistorialComentario historialComentario = new HistorialComentario();

            historialComentario.Escuela = new Escuela { ID = relevamientoModelo.Relevamiento.Escuela.ID };
            historialComentario.UserProfile = new Escuelas.NegocioEntidades.UserProfile { UserId = UsuarioActual.ObtenerUsuarioActual().UserId };
            historialComentario.FechaAlta = DateTime.Now;
            historialComentario.Comentarios = relevamientoModelo.Comentarios;

            historialComentarioComponente.GuardarComentario(historialComentario);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamiento.ID), tActivo = 7, mensaje = "Comentario Guardado" });
        }

        [Authorize(Roles = "Admin,Colaborador")]
        [HttpPost]
        public ActionResult EditarServicio(RelevamientoModelo relevamientoModelo)
        {


            relevamientoModelo.Servicio.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            servicioComponente.GuardarServicio(relevamientoModelo.Servicio);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Servicio.Relevamiento.ID), tActivo = 4, mensaje = "Servicio guardado" });
        }

        [HttpPost]
        public ActionResult EditarCapacitacion(RelevamientoModelo relevamientoModelo)
        {
            relevamientoModelo.Capacitacion.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            capacitacionComponente.GuardarCapacitacion(relevamientoModelo.Capacitacion);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Capacitacion.Relevamiento.ID), tActivo = 6, mensaje = "Capacitacion guardada" });
        }

        [HttpPost]
        public ActionResult EditarImagen(RelevamientoModelo relevamientoModelo)
        {
            relevamientoModelo.Imagen.Relevamiento = ObtenerOInsertarRelevamiento(relevamientoModelo);

            HttpPostedFileBase file = Request.Files["ImageData"];
            relevamientoModelo.Imagen.Foto = ConvertToBytes(file);

            imagenComponente.GuardarImagen(relevamientoModelo.Imagen);

            return RedirectToAction("EditarRelevamiento", new { relevamientoId = Encriptacion.EncriptarID(relevamientoModelo.Imagen.Relevamiento.ID), tActivo = 9, mensaje = "Imagen guardada" });
        }

        public ActionResult RetrieveImage(int id)
        {
            Imagen imagen = imagenComponente.ObtenerImagenPorId(id);

            if (imagen.Foto == null)
            {
                return null;
            }
            else
            {
                return File(imagen.Foto, "image/jpg");
            }

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public Relevamiento ObtenerOInsertarRelevamiento(RelevamientoModelo relevamientoModelo)
        {
            Relevamiento relevamiento;

            if (relevamientoModelo.Relevamiento.ID == 0)
            {
                relevamiento = new Relevamiento();
                relevamiento.Escuela = new Escuela() { ID = relevamientoModelo.Relevamiento.Escuela.ID };
                relevamiento.AtendidoPor = relevamientoModelo.Relevamiento.AtendidoPor;
                relevamiento.TieneADM = relevamientoModelo.Relevamiento.TieneADM;

                relevamientoComponente.GuardarRelevamiento(relevamiento);
            }
            else
            {
                relevamientoComponente.ActualizarFechaYModificadoPor(relevamientoModelo.Relevamiento.ID);

                relevamiento = new Relevamiento() { ID = relevamientoModelo.Relevamiento.ID };

            }

            return relevamiento;
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public List<Escuela> CargarEscuelasPorDistrito(int DistId)
        {

            return escuelaComponente.ObtenerEscuelasPorDistrito(DistId);
        }

       

        private List<SelectListItem> ObtenerGrados()
        {
            List<SelectListItem> listaGrados = new List<SelectListItem>();

            listaGrados.Add(new SelectListItem() { Value = "1", Text = "1° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "2", Text = "2° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "3", Text = "3° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "4", Text = "4° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "5", Text = "5° Grado" });
            listaGrados.Add(new SelectListItem() { Value = "6", Text = "6° Grado" });

            return listaGrados;
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarEscuelasPorDistrito2Async(int DistrId)
        {
            List<SelectListItem> listaEscuelas;

            if (DistrId > 0)
            {
                listaEscuelas = new List<SelectListItem>(CargarEscuelasPorDistrito(DistrId).OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Numero + " - " + item.Nombre }));
            }
            else
            {
                listaEscuelas = new List<SelectListItem>();
            }
            return Json(listaEscuelas, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarEscuelasPorDistritoAsync(string DistId)
        {
            List<SelectListItem> listaEscuelas;

            if (!string.IsNullOrEmpty(DistId))
            {
                listaEscuelas = new List<SelectListItem>(CargarEscuelasPorDistrito(Encriptacion.DesencriptarID(DistId)).OrderBy(e => e.Numero).ToList().Select(item => new SelectListItem { Value = Encriptacion.EncriptarID(item.ID), Text = item.Numero + " - " + item.Nombre }));
            }
            else
            {
                listaEscuelas = new List<SelectListItem>();
            }
            return Json(listaEscuelas, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult CargarDatosEscuelaAsync(int EscId)
        {

            Escuela escuela = escuelaComponente.ObtenerEscuelaPorId(EscId);

            string director = "";
            string viceDirector = "";

            if (escuela != null)
            {
                if (escuela.Director != null)
                    director = escuela.Director;

                if (escuela.ViceDirector != null)
                    viceDirector = escuela.ViceDirector;
            }

            string historialComentarios = historialComentarioComponente.ConstruirHistorialComentarios(historialComentarioComponente.ObtenerHistorialComentarioPorEscuela(EscId));

            string historialSegPedagogico = seguimientoPedagogicoComponente.ConstruirHistorialSegPedagogico(seguimientoPedagogicoComponente.ObtenerSeguimientoPedagogicoPorEscuela(EscId));

            return Json(new { Director = director, ViceDirector = viceDirector, HistorialComentarios = historialComentarios, HistorialSegPedagogico = historialSegPedagogico }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerMaquinaAsync(string MaqId)
        {
            Maquina maq = maquinaComponente.ObtenerMaquinaPorId(Encriptacion.DesencriptarID(MaqId));
            return Json(maq, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerDispositivoAsync(string DisId)
        {
            Dispositivo dis = dispositivoComponente.ObtenerDispositivoPorId(Encriptacion.DesencriptarID(DisId));

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion, TipoDispositivoId = dis.TipoDispositivo.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerSoftwareAsync(string SoftId)
        {
            Software soft = softwareComponente.ObtenerSoftwarePorId(Encriptacion.DesencriptarID(SoftId));

            return Json(new { ID = soft.ID, Nombre = soft.Nombre, Descripcion = soft.Descripcion, TipoSoftwareId = soft.TipoSoftware.ID, PlataformaSoftwareId = soft.PlataformaSoftware.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerServicioAsync(string SerId)
        {
            Servicio ser = servicioComponente.ObtenerServicioPorId(Encriptacion.DesencriptarID(SerId));

            return Json(new { ID = ser.ID, Compañia = ser.Compañia, EsPago = ser.EsPago, Descripcion = ser.Descripcion, TipoServicioId = ser.TipoServicio.ID }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerCapacitacionAsync(string CapId)
        {
            Capacitacion cap = capacitacionComponente.ObtenerCapacitacionPorId(Encriptacion.DesencriptarID(CapId));

            return Json(new { ID = cap.ID, Curso = cap.Curso, Descripcion = cap.Descripcion, Grado = cap.Grado }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerImagenAsync(string ImgId)
        {
            Imagen img = imagenComponente.ObtenerImagenPorId(Encriptacion.DesencriptarID(ImgId));

            return Json(new { ID = img.ID, Titulo = img.Titulo, Descripcion = img.Descripcion, Contenido = img.Contenido, Foto = img.Foto }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Colaborador")]
        public ActionResult ObtenerDispositivoRedAsync(string DisRedId)
        {
            DispositivoRed dis = dispositivoRedComponente.ObtenerDispositivoRedPorId(Encriptacion.DesencriptarID(DisRedId));

            return Json(new { ID = dis.ID, Marca = dis.Marca, Modelo = dis.Modelo, Descripcion = dis.Descripcion, Ubicacion = dis.Ubicacion, PuertosUtilizados = dis.PuertosUtilizados, PuertosTotales = dis.PuertosTotales, Protocolo = dis.Protocolo, TipoDispositivoRedId = dis.TipoDispositivoRed.ID }, JsonRequestBehavior.AllowGet);
        }

       
    }
}
