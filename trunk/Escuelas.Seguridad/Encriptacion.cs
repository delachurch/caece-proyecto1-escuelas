using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.Seguridad
{
    public static class Encriptacion
    {
        public static string Encriptar(string clave)
        {
            byte[] resultado = System.Text.Encoding.UTF8.GetBytes(clave);

            return Convert.ToBase64String(resultado);
        }

        public static string Desencriptar(string claveEnc)
        {
            byte[] encriptado = Convert.FromBase64String(claveEnc);

            return System.Text.Encoding.UTF8.GetString(encriptado);
        }

        public static string EncriptarID(int id)
        {
            return Encriptar(id.ToString());
        }

        public static int DesencriptarID(string claveEnc)
        {
            return int.Parse(Desencriptar(claveEnc));
        }
    }
}
