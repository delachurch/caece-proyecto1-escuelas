﻿using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class ServicioComponente
    {
        ServicioDA servicioDA = new ServicioDA();

        public Servicio ObtenerServicioPorId(int serId)
        {
            return servicioDA.ObtenerServicioPorId(serId);
        }

        public void GuardarServicio(Servicio servicio)
        {

            if (servicio.ID > 0)
            {
                servicioDA.ActualizarServicio(servicio);
            }
            else
            {
                servicioDA.InsertarServicio(servicio);
            }
        }

        public void BorrarServicio(int serId)
        {
            servicioDA.BorrarServicio(serId);
        }
    }
}
