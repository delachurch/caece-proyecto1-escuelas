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
    public class UsuarioComponente
    {
        UsuarioDA usuarioDA = new UsuarioDA();
        public UserProfile ObtenerUsuarioPorNombre(string nombre)
        {
            return usuarioDA.ObtenerUsuarioPorNombre(nombre);
        }
    }
}
