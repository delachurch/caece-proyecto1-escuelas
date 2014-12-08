using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class Categoria
    {
        public Categoria()
        {
            CategoriaValores = new List<CategoriaValor>();
        }
        public int ID { get; set; }
        [Display(Name = "Categoría")]
        public string Nombre { get; set; }
        public List<CategoriaValor> CategoriaValores { get; set; }
    }
}
