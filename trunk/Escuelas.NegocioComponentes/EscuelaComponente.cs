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
    public class EscuelaComponente
    {
        EscuelaDA escuelaDA = new EscuelaDA();

        public List<Escuela> ObtenerEscuelas()
        {
            return escuelaDA.ObtenerEscuelas();
        }
        public Escuela ObtenerEscuelaPorId(int escuelaId)
        {
            return escuelaDA.ObtenerEscuelaPorId(escuelaId);
        }
        public void GuardarEscuela(Escuela escuela)
        {
            if (escuela.ID > 0)
            {
                escuela.FechaModificacion = DateTime.Now;
                escuelaDA.ActualizarEscuela(escuela);
            }
            else
            {
                escuela.FechaAlta = DateTime.Now;

                escuela.TipoEstablecimiento = new CategoriaValor { ID = Enums.TipoEstablecimiento.Primario.GetHashCode() };
                
                escuelaDA.InsertarEscuela(escuela);    
            }
            
        }
    }
}
