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
    public class HistorialComentarioComponente
    {
        HistorialComentarioDA historialComentarioDA = new HistorialComentarioDA();
        public List<HistorialComentario> ObtenerHistorialComentarioPorEscuela(int escId)
        {
          return historialComentarioDA.ObtenerHistorialComentarioPorEscuela(escId);
        }

        public void GuardarComentario(HistorialComentario nuevoHistorialComentario)
        {
            historialComentarioDA.InsertarComentario(nuevoHistorialComentario);
        }
     
    }
}
