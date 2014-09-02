using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Escuelas.Seguridad
{
    public static class UsuarioActual
    {
        

        public static UserProfile ObtenerUsuarioActual()
        {
            UserProfile userProfile = new UserProfile();
        
            Object usuarioCacheado = HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name];
            
            if(usuarioCacheado == null)
            {
                userProfile = new UsuarioDA().ObtenerUsuarioPorNombre(HttpContext.Current.User.Identity.Name);

                HttpContext.Current.Cache.Insert(HttpContext.Current.User.Identity.Name, userProfile, null, System.Web.Caching.Cache.NoAbsoluteExpiration,TimeSpan.FromSeconds(1200));
            }
            else
            {
                userProfile = (UserProfile)usuarioCacheado;
            }

            return userProfile;
        
        }
    }
}
