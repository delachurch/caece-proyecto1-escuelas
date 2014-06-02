using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class DispositivoComponente
    {
        DispositivoDA dispositivoDA = new DispositivoDA();

        public Dispositivo ObtenerDispositivoPorId(int disId)
        {
            return dispositivoDA.ObtenerDispositivoPorId(disId);
        }


        public void GuardarDispositivo(Dispositivo dispositivo)
        {

            if (dispositivo.ID > 0)
            {
                dispositivoDA.ActualizarDispositivo(dispositivo);
            }
            else
            {
                dispositivoDA.InsertarDispositivo(dispositivo);
            }
        }

        public void BorrarDispositivo(int disId)
        {
            dispositivoDA.BorrarDispositivo(disId);
        }
    }
}
