using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.Comun
{
    public class Enums
    {
        public enum TipoEstablecimiento { Primario = 1, Secundario = 2,PreEscolar= 3,Otro = 4 };
        public enum Categoria { TipoEstablecimiento = 1, TipoDispositivo = 2, TipoServicio = 3,TipoDispositivoRed = 4, TipoSoftware = 5};
    }
}
