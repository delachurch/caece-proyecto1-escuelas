using Escuelas.AccesoADatos;
using Escuelas.Comun;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Escuelas.NegocioComponentes
{
    public class RelevamientoComponente
    {
        RelevamientoDA relevamientoDA = new RelevamientoDA();

        public List<Relevamiento> ObtenerRelevamientos()
        {
            return relevamientoDA.ObtenerRelevamientos();    
        }

        public Relevamiento ObtenerRelevamientoPorId(int relevamientoId)
        {
            return relevamientoDA.ObtenerRelevamientoPorId(relevamientoId);
        }

        public Relevamiento ObtenerUltimoRelevamiento(int escuelaId)
        {
            return relevamientoDA.ObtenerUltimoRelevamiento(escuelaId);
        }

        public List<Relevamiento> ObtenerRelevamientosPorDistrito(int distId)
        {
            return relevamientoDA.ObtenerRelevamientosPorDistrito(distId);
        }
        public List<Relevamiento> ObtenerRelevamientosPorEscuela(int escId)
        {
            return relevamientoDA.ObtenerRelevamientosPorEscuela(escId);
        }

        public void CopiarRelevamiento(Relevamiento relevamiento)
        {
            relevamientoDA.CopiarRelevamiento(relevamiento);
        }
        public void GuardarRelevamiento(Relevamiento relevamiento)
        {
            relevamiento.Escuela.Distrito = null;
           
            if (relevamiento.ID > 0)
            {
                relevamiento.FechaModificacion = DateTime.Now;
                relevamientoDA.ActualizarRelevamiento(relevamiento);
            }
            else
            {
                relevamiento.FechaRelevo = DateTime.Now;

                relevamientoDA.InsertarRelevamiento(relevamiento);
            }
        }
    }
}
