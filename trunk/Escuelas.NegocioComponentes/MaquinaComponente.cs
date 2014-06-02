using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class MaquinaComponente
    {
        MaquinaDA maquinaDA = new MaquinaDA();

        public Maquina ObtenerMaquinaPorId(int maquinaId)
        {
            return maquinaDA.ObtenerMaquinaPorId(maquinaId);
        }
        
        public void GuardarMaquina(Maquina maquina)
        {

            if (maquina.ID > 0)
            {
                maquinaDA.ActualizarMaquina(maquina);
            }
            else
            {
                maquinaDA.InsertarMaquina(maquina);
            }
        }

        public void BorrarMaquina(int maqId)
        {
            maquinaDA.BorrarMaquina(maqId);
        }
    }
}
