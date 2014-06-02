using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class DispositivoDA
    {

        public Dispositivo ObtenerDispositivoPorId(int disId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Dispositivos.Include("TipoDispositivo").Where(d => d.ID == disId).SingleOrDefault();
            }
        }

        public void InsertarDispositivo(Dispositivo nuevoDispositivo)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevoDispositivo.Relevamiento);

                contexto.CategoriaValores.Attach(nuevoDispositivo.TipoDispositivo);

                contexto.Dispositivos.Add(nuevoDispositivo);

                contexto.SaveChanges();
            }
        }

        public void ActualizarDispositivo(Dispositivo nuevoDispositivo)
        {
            using (Contexto contexto = new Contexto())
            {
                Dispositivo dispositivo = contexto.Dispositivos.Include("Relevamiento").Include("TipoDispositivo").Where(d => d.ID == nuevoDispositivo.ID).SingleOrDefault();


                dispositivo.Descripcion = nuevoDispositivo.Descripcion;
                dispositivo.Marca = nuevoDispositivo.Marca;
                dispositivo.Modelo = nuevoDispositivo.Modelo;
                dispositivo.Ubicacion = nuevoDispositivo.Ubicacion;

                if (dispositivo.TipoDispositivo.ID != nuevoDispositivo.TipoDispositivo.ID)
                {
                    dispositivo.TipoDispositivo = nuevoDispositivo.TipoDispositivo;
                    contexto.CategoriaValores.Attach(dispositivo.TipoDispositivo);
                }

                contexto.Entry(dispositivo).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarDispositivo(int disId)
        {
            using (Contexto contexto = new Contexto())
            {
                Dispositivo dispositivo = contexto.Dispositivos.Where(d => d.ID == disId).SingleOrDefault();

                contexto.Dispositivos.Remove(dispositivo);

                contexto.SaveChanges();
            }
        }
    }
}
