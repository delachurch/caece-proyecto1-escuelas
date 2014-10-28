using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Escuelas.NegocioEntidades;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Escuelas.AccesoADatos
{
    public class Contexto : DbContext 
    {
        public Contexto() : base("EscuelasConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Relevamiento> Relevamientos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Personal> ListaPersonal { get; set; }
        public DbSet<Categoria> Categorias{ get; set; }
        public DbSet<CategoriaValor> CategoriaValores { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Software> Softwares{ get; set; }
        public DbSet<Capacitacion> Capacitaciones { get; set; }
        public DbSet<DispositivoRed> DispositivosRed { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<HistorialComentario> HistorialComentarios { get; set; }
        public DbSet<SeguimientoPedagogico> SeguimientoPedagogicos { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Contexto>(null);

            modelBuilder.Entity<Escuela>().HasRequired(e => e.Distrito).WithMany(d => d.Escuelas).Map(x => x.MapKey("DistritoId"));
            modelBuilder.Entity<Escuela>().HasRequired(e => e.TipoEstablecimiento).WithMany(te => te.Escuelas).Map(x => x.MapKey("TipoEstablecimientoId"));

            modelBuilder.Entity<CategoriaValor>().HasRequired(cv=> cv.Categoria).WithMany(cv => cv.CategoriaValores).Map(x => x.MapKey("CategoriaId"));

            modelBuilder.Entity<Dispositivo>().HasRequired(d => d.TipoDispositivo).WithMany(td => td.Dispositivos).Map(x => x.MapKey("TipoDispositivoId"));
            modelBuilder.Entity<Dispositivo>().HasRequired(d => d.Relevamiento).WithMany(r => r.Dispositivos).Map(x => x.MapKey("RelevamientoId"));

            modelBuilder.Entity<Maquina>().HasRequired(m => m.Relevamiento).WithMany(r => r.Maquinas).Map(x => x.MapKey("RelevamientoId"));

            modelBuilder.Entity<Personal>().HasRequired(p => p.Escuela).WithMany(e => e.ListaPersonal).Map(x => x.MapKey("EscuelaId"));

            modelBuilder.Entity<Relevamiento>().HasRequired(r => r.Escuela).WithMany(e => e.Relevamientos).Map(x => x.MapKey("EscuelaId"));
            modelBuilder.Entity<Relevamiento>().HasRequired(r => r.CreadoPor).WithMany(e => e.RelevamientosCreados).Map(x => x.MapKey("CreadoPor"));
            modelBuilder.Entity<Relevamiento>().HasOptional(r => r.ModificadoPor).WithMany(e => e.RelevamientosModificados).Map(x => x.MapKey("ModificadoPor"));

            modelBuilder.Entity<Servicio>().HasRequired(s => s.TipoServicio).WithMany(ts => ts.Servicios).Map(x => x.MapKey("TipoServicioId"));
            modelBuilder.Entity<Servicio>().HasRequired(s => s.Relevamiento).WithMany(r => r.Servicios).Map(x => x.MapKey("RelevamientoId"));

            modelBuilder.Entity<DispositivoRed>().HasRequired(d => d.TipoDispositivoRed).WithMany(td => td.DispositivosRed).Map(x => x.MapKey("TipoDispositivoRedId"));
            modelBuilder.Entity<DispositivoRed>().HasRequired(d => d.Relevamiento).WithMany(d => d.DispositivosRed).Map(x => x.MapKey("RelevamientoId"));

            modelBuilder.Entity<Software>().HasRequired(s => s.TipoSoftware).WithMany(ts => ts.Softwares).Map(x => x.MapKey("TipoSoftwareId"));
            modelBuilder.Entity<Software>().HasRequired(s => s.PlataformaSoftware).WithMany(ts => ts.SoftwaresDePlataforma).Map(x => x.MapKey("PlataformaSoftwareId"));
            modelBuilder.Entity<Software>().HasRequired(s => s.Relevamiento).WithMany(r => r.Softwares).Map(x => x.MapKey("RelevamientoId"));

            modelBuilder.Entity<Capacitacion>().HasRequired(c => c.Relevamiento).WithMany(c => c.Capacitaciones).Map(x => x.MapKey("RelevamientoId"));
            
            modelBuilder.Entity<Usuario>().HasRequired(u => u.Distrito).WithMany(d => d.Usuarios).Map(x => x.MapKey("DistritoId"));
            modelBuilder.Entity<Usuario>().HasRequired(u => u.Rol).WithMany(r => r.Usuarios).Map(x => x.MapKey("RolId"));

            modelBuilder.Entity<HistorialComentario>().HasRequired(h => h.Escuela).WithMany(e => e.HistorialComentarios).Map(x => x.MapKey("EscuelaId"));
            modelBuilder.Entity<HistorialComentario>().HasRequired(h => h.UserProfile).WithMany(u => u.HistorialComentarios).Map(x => x.MapKey("UserProfileId"));

            modelBuilder.Entity<SeguimientoPedagogico>().HasRequired(h => h.Escuela).WithMany(e => e.SeguimientoPedagogicos).Map(x => x.MapKey("EscuelaId"));
            modelBuilder.Entity<SeguimientoPedagogico>().HasRequired(h => h.UserProfile).WithMany(u => u.SeguimientoPedagogicos).Map(x => x.MapKey("UserProfileId"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
