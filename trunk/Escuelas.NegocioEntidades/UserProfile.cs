using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.NegocioEntidades
{
    [Serializable]
    public class UserProfile
    {
        public UserProfile()
        {
            HistorialComentarios = new List<HistorialComentario>();
        }
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<HistorialComentario> HistorialComentarios { get; set; }
    }
}
