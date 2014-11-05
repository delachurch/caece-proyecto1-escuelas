using Escuelas.NegocioEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuelas.AccesoADatos
{
    public class MaquinaDA
    {
        public Maquina ObtenerMaquinaPorId(int maquinaId)
        {
            using (Contexto contexto = new Contexto())
            {
                return contexto.Maquinas.Where(m => m.ID == maquinaId).SingleOrDefault();
            }            
        }
        public void InsertarMaquina(Maquina nuevaMaquina)
        {
            using (Contexto contexto = new Contexto())
            {

                contexto.Relevamientos.Attach(nuevaMaquina.Relevamiento);

                contexto.Maquinas.Add(nuevaMaquina);

                contexto.SaveChanges();
            }
        }

        public void ActualizarMaquina(Maquina nuevaMaquina)
        {
            using (Contexto contexto = new Contexto())
            {
                Maquina maquina = contexto.Maquinas.Include("Relevamiento").Where(m => m.ID == nuevaMaquina.ID).SingleOrDefault();

                maquina.Marca = nuevaMaquina.Marca;
                maquina.Nombre = nuevaMaquina.Nombre;
                maquina.Procesador = nuevaMaquina.Procesador;
                maquina.SistemaOperativo = nuevaMaquina.SistemaOperativo;
                maquina.PerteneceARed = nuevaMaquina.PerteneceARed;
                maquina.PlacaVideo = nuevaMaquina.PlacaVideo;
                maquina.TieneMicrofono = nuevaMaquina.TieneMicrofono;
                maquina.CapacidadDiscoDuro = nuevaMaquina.CapacidadDiscoDuro;
                maquina.DispositivoSonido = nuevaMaquina.DispositivoSonido;
                maquina.MemoriaRAM = nuevaMaquina.MemoriaRAM;
                maquina.Comentarios = nuevaMaquina.Comentarios;
                maquina.CDLectGrab = nuevaMaquina.CDLectGrab;
                maquina.DVDLectGrab = nuevaMaquina.DVDLectGrab;
                maquina.Funciona = nuevaMaquina.Funciona;
                maquina.Disquetera = nuevaMaquina.Disquetera;
                maquina.Monitor = nuevaMaquina.Monitor;
                maquina.Webcam = nuevaMaquina.Webcam;
                maquina.GrupoDeTrabajo = nuevaMaquina.GrupoDeTrabajo;
                maquina.IP = nuevaMaquina.IP;
                maquina.Ubicacion = nuevaMaquina.Ubicacion;
                maquina.PuertosUSBFrontales = nuevaMaquina.PuertosUSBFrontales;
                maquina.PuertosUSBTraseros = nuevaMaquina.PuertosUSBTraseros;

                contexto.Entry(maquina).State = System.Data.EntityState.Modified;

                contexto.SaveChanges();

            }
        }

        public void BorrarMaquina(int maqId)
        {
            using (Contexto contexto = new Contexto())
            {
                Maquina maquina = contexto.Maquinas.Where(m => m.ID == maqId).SingleOrDefault();

                contexto.Maquinas.Remove(maquina);

                contexto.SaveChanges();
            }
        }

    }
}
