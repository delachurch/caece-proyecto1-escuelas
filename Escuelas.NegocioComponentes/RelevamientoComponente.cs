﻿using Escuelas.AccesoADatos;
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

        public List<Relevamiento> ObtenerRelevamientosPorDistrito(int distId)
        {
            return relevamientoDA.ObtenerRelevamientosPorDistrito(distId);
        }
        public List<Relevamiento> ObtenerRelevamientosPorEscuela(int escId)
        {
            return relevamientoDA.ObtenerRelevamientosPorEscuela(escId);
        }
        public void GuardarRelevamiento(Relevamiento relevamiento)
        {
            relevamiento.Escuela.Distrito = null;
           
            relevamiento.TieneADM = false;
           
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
