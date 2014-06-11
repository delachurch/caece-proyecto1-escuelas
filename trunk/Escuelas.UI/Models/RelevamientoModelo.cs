﻿using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuelas.UI.Models
{
    [Serializable]
    public class RelevamientoModelo
    {
        public Relevamiento Relevamiento { get; set; }
        public Maquina Maquina { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public DispositivoRed DispositivoRed { get; set; }
        public Servicio Servicio { get; set; }
        public int TabActivo { get; set; }
    }
}