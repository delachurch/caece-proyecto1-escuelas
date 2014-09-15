using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class SoftwareComponente
    {
        SoftwareDA SoftwareDA = new SoftwareDA();

        public Software ObtenerSoftwarePorId(int disId)
        {
            return SoftwareDA.ObtenerSoftwarePorId(disId);
        }


        public void GuardarSoftware(Software Software)
        {

            if (Software.ID > 0)
            {
                SoftwareDA.ActualizarSoftware(Software);
            }
            else
            {
                SoftwareDA.InsertarSoftware(Software);
            }
        }

        public void BorrarSoftware(int disId)
        {
            SoftwareDA.BorrarSoftware(disId);
        }
    }
}
