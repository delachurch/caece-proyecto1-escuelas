using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class EscuelaDA
    {
        public List<Escuela> ObtenerEscuelas()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Escuelas.ToList();
            }
        }
    }
}
