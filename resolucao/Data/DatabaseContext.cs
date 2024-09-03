using Desafio_UOL.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_UOL.Data
{
    public class DatabaseContext : DbContext
    {

        public virtual DbSet<JogadorModel> Jogadores { get; set; }
        public virtual DbSet<GrupoModel> Grupos { get; set; }
        public virtual DbSet<CodinomeModel> Codinomes { get; set; }


        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JogadorModel>(entity =>
            {
                entity.ToTable("T_UOL_JOGADOR");
                entity.HasKey(j => j.Id);
                entity.HasOne(j => j.Codinome)
                .WithOne(c => c.Jogador)
                .HasForeignKey<JogadorModel>(j => j.CodinomeId);

                entity.Property(j => j.CodinomeId).IsRequired();
                entity.Property(j => j.Nome).IsRequired();
                entity.Property(j => j.Email).IsRequired();
                entity.Property(j => j.Telefone).IsRequired(false);
                entity.HasIndex(j => j.Email).IsUnique();
                entity.HasIndex(j => j.CodinomeId).IsUnique();
            });

            modelBuilder.Entity<CodinomeModel>(entity =>
            {
                entity.ToTable("T_UOL_CODINOME");
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Grupo)
                .WithMany(g => g.Codinomes)
                .HasForeignKey(c => c.GrupoId);

                entity.Property(c => c.Nome).IsRequired();
                entity.Property(c => c.GrupoId).IsRequired();
            });

            modelBuilder.Entity<GrupoModel>(entity =>
            {
                entity.ToTable("T_UOL_GRUPO");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Nome).IsRequired();
                entity.HasIndex(g => g.Nome).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

    }

}
