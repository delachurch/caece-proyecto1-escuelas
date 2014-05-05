using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class CategoriaValorDA
    {
        public List<CategoriaValor> ObtenerCategoriasValor(int categoriaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.CategoriaValores.Where(c => c.Categoria.ID == categoriaId).OrderBy(c => c.Nombre).ToList();
            }
        }
    }
}
