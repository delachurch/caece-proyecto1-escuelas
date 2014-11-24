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
        public List<Distrito> ObtenerDistritosPorRegion(int region)
        {
            return distritoDA.ObtenerDistritosPorRegion(region);
        }
        public Distrito ObtenerDistritoPorId(int distritoId)
        {
            return distritoDA.ObtenerDistritoPorId(distritoId);
        }

        public void GuardarDistrito(Distrito distrito)
        {
            if (distrito.ID > 0)
            {
                distrito.Inactivo = false;

                distritoDA.ActualizarDistrito(distrito);
            }
            else
            {
                distritoDA.InsertarDistrito(distrito);
            }

        }

        public void BorrarDistrito(int distritoId)
        {
            distritoDA.BorrarDistrito(distritoId);
        }

    }
}
