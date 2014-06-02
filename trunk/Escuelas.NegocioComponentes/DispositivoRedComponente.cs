using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class DispositivoRedComponente
    {
        DispositivoRedDA dispositivoRedDA = new DispositivoRedDA();

        public DispositivoRed ObtenerDispositivoRedPorId(int disId)
        {
            return dispositivoRedDA.ObtenerDispositivoRedPorId(disId);
        }


        public void GuardarDispositivoRed(DispositivoRed dispositivoRed)
        {

            if (dispositivoRed.ID > 0)
            {
                dispositivoRedDA.ActualizarDispositivoRed(dispositivoRed);
            }
            else
            {
                dispositivoRedDA.InsertarDispositivoRed(dispositivoRed);
            }
        }

        public void BorrarDispositivoRed(int disId)
        {
            dispositivoRedDA.BorrarDispositivoRed(disId);
        }
    }
}
