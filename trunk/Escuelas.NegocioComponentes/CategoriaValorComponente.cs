using Escuelas.AccesoADatos;
using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioComponentes
{
    public class CategoriaValorComponente
    {
        CategoriaValorDA categoriaValorDA = new CategoriaValorDA();

        public List<CategoriaValor> ObtenerCategoriasValor(int categoriaId)
        {
            return categoriaValorDA.ObtenerCategoriasValor(categoriaId);
        }
    }
}
