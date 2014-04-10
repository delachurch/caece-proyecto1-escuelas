using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class EscuelaComponente
    {
        EscuelaDA escuelaDA = new EscuelaDA();

        public List<Escuela> ObtenerEscuelas()
        {
            return escuelaDA.ObtenerEscuelas();
        }
    }
}
