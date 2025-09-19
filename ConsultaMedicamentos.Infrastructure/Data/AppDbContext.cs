using ConsultaMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<PracticaMedica> practicaMedicas { get; set; }
        public DbSet<ConsumoMedico> consumoMedicos { get; set; }

        public DbSet<DesconocimientosSociales> desconocimientosSociales { get; set; }

        public DbSet<ParametrosApi> parametrosApi { get; set; }

        public DbSet<Matriculado> matriculado {  get; set; }

        public DbSet<Afiliado> Afiliados { get; set; } = null!;

        public DbSet<PracticaPersona> practicaPersona { get; set; }
        public DbSet<MatriculadoPersona> martriculadoPersona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PracticaMedica>().HasNoKey();
            modelBuilder.Entity<ConsumoMedico>().HasNoKey();
            //modelBuilder.Entity<DesconocimientosSociales>()
            //    .Property(d => d.Id)
            //    .ValueGeneratedOnAdd();

            modelBuilder.Entity<ParametrosApi>().HasNoKey();
            modelBuilder.Entity<Afiliado>().HasNoKey();
            modelBuilder.Entity<PracticaPersona>().HasNoKey();
            modelBuilder.Entity<MatriculadoPersona>().HasNoKey();

        }

    }
}
