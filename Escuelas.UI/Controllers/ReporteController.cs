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
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.DataVisualization;
using System.IO;
using System.Drawing;

namespace Escuelas.UI.Controllers
{
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/
        EscuelaComponente escuelaComponente = new EscuelaComponente();
        DistritoComponente distritoComponente = new DistritoComponente();
        RelevamientoComponente relevamientoComponente = new RelevamientoComponente();


        public ActionResult RelevamientoporEscuela()
        {
            return View();
        }

        public ChartArea CreateChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "Resultado";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Interval = 1;

            return chartArea;
        }



#region RelevamientoporEscuela

        public List<RelevamientoporEscuela> ListaRelevamientoporEscuela()
        {
            List<Relevamiento> relevamientos = relevamientoComponente.ObtenerRelevamientos();
            IEnumerable<RelevamientoporEscuela> relevamientoporEscuela = new List<RelevamientoporEscuela>();
            relevamientoporEscuela = from r in relevamientos
                                     group r by new { r.Escuela, r.FechaRelevo.Year } into eg
                                     select new RelevamientoporEscuela()
                                     {
                                         Escuela = eg.Key.Escuela,
                                         Anio = eg.Key.Year,
                                         Total = eg.Count()
                                     };
            return relevamientoporEscuela.ToList();
        }

        public FileResult CrearRelevamientoporEscuela()
        {
            IList<RelevamientoporEscuela> escuelas = ListaRelevamientoporEscuela();
            Chart chart = new Chart();
            chart.BorderSkin.BackColor = Color.Transparent;
            chart.BorderSkin.PageColor = Color.Transparent;
            chart.Width = 700;
            chart.Height = 300;
            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle("Relevamiento por Escuela"));
            //chart.Legends.Add(CreateLegend());
            chart.Series.Add(CrearSerieRelevamientoporEscuela(escuelas, SeriesChartType.Column));
            chart.ChartAreas.Add(CreateChartArea());

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        public Title CreateTitle(string titulo)
        {
            Title title = new Title();
            title.Text = titulo;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            return title;
        }

        public Series CrearSerieRelevamientoporEscuela(IList<RelevamientoporEscuela> results, SeriesChartType chartType)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = "Numero de Relevamientos";
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.LimeGreen;
            seriesDetail.ChartType = chartType;
            seriesDetail.BorderWidth = 2;
            DataPoint point;

            foreach (RelevamientoporEscuela result in results)
            {
                point = new DataPoint();
                point.AxisLabel = string.Concat("Nro ",result.Escuela.Numero.ToString(), " - ", result.Escuela.Nombre);
                point.YValues = new double[] { (result.Total) };
                seriesDetail.Points.Add(point);
            }

            seriesDetail.ChartArea = "Resultado";
            return seriesDetail;
        }

#endregion


    }
}
