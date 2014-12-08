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
        public List<CategoriaValor> ObtenerCategoriasValorPorCategoria(int categoriaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.CategoriaValores.Where(c => c.Categoria.ID == categoriaId && c.Inactivo == false).OrderBy(c => c.Nombre).ToList();
            }
        }

        public List<CategoriaValor> ObtenerCategoriasValor()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.CategoriaValores.Include("Categoria").Where(c => c.Inactivo == false).OrderBy(c => c.Categoria.Nombre).ThenBy(c => c.Nombre).ToList();
            }
        }

        public List<Categoria> ObtenerCategorias()
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Categorias.OrderBy(c => c.Nombre).ToList();
            }
        }

        public CategoriaValor ObtenerCategoriaValorPorId(int catValorId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.CategoriaValores.Include("Categoria").Where(cv => cv.ID == catValorId).SingleOrDefault();
            }
        }
        public void InsertarCategoriaValor(CategoriaValor nuevoCategoriaValor)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Categorias.Attach(nuevoCategoriaValor.Categoria);

                contexto.CategoriaValores.Add(nuevoCategoriaValor);

                contexto.SaveChanges();
            }
        }
        public void ActualizarCategoriaValor(CategoriaValor nuevoCategoriaValor)
        {
            using (Contexto contexto = new Contexto())
            {
                CategoriaValor categoriaValor = contexto.CategoriaValores.Where(d => d.ID == nuevoCategoriaValor.ID).SingleOrDefault();

                categoriaValor.Nombre = nuevoCategoriaValor.Nombre;

                contexto.Entry(categoriaValor).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarCategoriaValor(int CategoriaValorId)
        {
            using (Contexto contexto = new Contexto())
            {
                CategoriaValor categoriaValor = contexto.CategoriaValores.Where(d => d.ID == CategoriaValorId).SingleOrDefault();

                categoriaValor.Inactivo = true;

                contexto.Entry(categoriaValor).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();
            }
        }

    }
}
