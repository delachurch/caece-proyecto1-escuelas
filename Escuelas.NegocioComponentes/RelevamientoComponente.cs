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
        
    }
}
