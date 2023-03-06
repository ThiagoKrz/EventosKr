using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrEventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace KrEventos.Persistence.Contextos
{
    public class KrEventosContext : DbContext
    {
        public KrEventosContext(DbContextOptions<KrEventosContext> options) 
        : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new {PE.EventoId, PE.PalestranteId});
        }

        internal void RemoveRange(object entityArray)
        {
            throw new NotImplementedException();
        }
    }
}