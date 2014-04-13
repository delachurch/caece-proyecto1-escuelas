using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class DistritoDA
    {
        public List<Distrito> ObtenerDistritos()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Distritos.OrderBy(d => d.Nombre).ToList();
            }
        }
        public List<Distrito> ObtenerDistritosPorRegion(int regionId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Distritos.OrderBy(d => d.Nombre).ToList();
            }
        }
    }
}
