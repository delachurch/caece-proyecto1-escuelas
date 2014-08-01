using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class UsuarioDA
    {
        public UserProfile ObtenerUsuarioPorNombre(string nombre)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.UserProfiles.Where(u => u.UserName == nombre).SingleOrDefault();
            }
        }
    }
}
