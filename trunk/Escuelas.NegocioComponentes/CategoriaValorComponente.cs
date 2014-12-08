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

        public List<CategoriaValor> ObtenerCategoriasValorPorCategoria(int categoriaId)
        {
            return categoriaValorDA.ObtenerCategoriasValorPorCategoria(categoriaId);
        }

        public List<CategoriaValor> ObtenerCategoriasValor()
        {
            return categoriaValorDA.ObtenerCategoriasValor();
        }
        public CategoriaValor ObtenerCategoriaValorPorId(int catValorId)
        {
            return categoriaValorDA.ObtenerCategoriaValorPorId(catValorId);
        }

        public List<Categoria> ObtenerCategorias()
        {
            return categoriaValorDA.ObtenerCategorias();
        }

        public void GuardarCategoriaValor(CategoriaValor categoriaValor)
        {
            if (categoriaValor.ID > 0)
            {
                categoriaValorDA.ActualizarCategoriaValor(categoriaValor);
            }
            else
            {
                categoriaValorDA.InsertarCategoriaValor(categoriaValor);
            }

        }

        public void BorrarCategoriaValor(int categoriaValorId)
        {
            categoriaValorDA.BorrarCategoriaValor(categoriaValorId);
        }
    }
}
