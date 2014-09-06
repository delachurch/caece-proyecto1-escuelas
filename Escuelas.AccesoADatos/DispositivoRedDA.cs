using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class DispositivoRedDA
    {
        public DispositivoRed ObtenerDispositivoRedPorId(int disId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.DispositivosRed.Include("TipoDispositivoRed").Where(d => d.ID == disId).SingleOrDefault();
            }
        }

        public void InsertarDispositivoRed(DispositivoRed nuevoDispositivoRed)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevoDispositivoRed.Relevamiento);

                contexto.CategoriaValores.Attach(nuevoDispositivoRed.TipoDispositivoRed);

                contexto.DispositivosRed.Add(nuevoDispositivoRed);

                contexto.SaveChanges();
            }
        }

        public void ActualizarDispositivoRed(DispositivoRed nuevoDispositivoRed)
        {
            using (Contexto contexto = new Contexto())
            {
                DispositivoRed dispositivoRed = contexto.DispositivosRed.Include("Relevamiento").Include("TipoDispositivoRed").Where(d => d.ID == nuevoDispositivoRed.ID).SingleOrDefault();


                dispositivoRed.Descripcion = nuevoDispositivoRed.Descripcion;
                dispositivoRed.Marca = nuevoDispositivoRed.Marca;
                dispositivoRed.Modelo = nuevoDispositivoRed.Modelo;
                dispositivoRed.Ubicacion = nuevoDispositivoRed.Ubicacion;
                dispositivoRed.PuertosUtilizados = nuevoDispositivoRed.PuertosUtilizados;
                dispositivoRed.PuertosTotales = nuevoDispositivoRed.PuertosTotales;
                dispositivoRed.Protocolo = nuevoDispositivoRed.Protocolo;

                if (dispositivoRed.TipoDispositivoRed.ID != nuevoDispositivoRed.TipoDispositivoRed.ID)
                {
                    dispositivoRed.TipoDispositivoRed = nuevoDispositivoRed.TipoDispositivoRed;
                    contexto.CategoriaValores.Attach(dispositivoRed.TipoDispositivoRed);
                }

                contexto.Entry(dispositivoRed).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarDispositivoRed(int disId)
        {
            using (Contexto contexto = new Contexto())
            {
                DispositivoRed dispositivoRed = contexto.DispositivosRed.Where(d => d.ID == disId).SingleOrDefault();

                contexto.DispositivosRed.Remove(dispositivoRed);

                contexto.SaveChanges();
            }
        }
    }
}
