using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class DistritoComponente
    {
        DistritoDA distritoDA = new DistritoDA();
        public List<Distrito> ObtenerDistritos()
        {
            return distritoDA.ObtenerDistritos();
        }
    }
}
