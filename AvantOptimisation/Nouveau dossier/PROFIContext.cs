using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Nouveau_dossier
{
    public partial class PROFIContext : DbContext
    {
        public PROFIContext()
        {
        }

        public PROFIContext(DbContextOptions<PROFIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contrat> Contrats { get; set; }
        public virtual DbSet<Personne> Personnes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PROFI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Contrat>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("CONTRAT");

                entity.Property(e => e.Uid)
                    .HasMaxLength(32)
                    .HasColumnName("uid")
                    .IsFixedLength();

                entity.Property(e => e.Debut)
                    .HasColumnType("datetime")
                    .HasColumnName("debut");

                entity.Property(e => e.Montant)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("montant");

                entity.Property(e => e.Reduction)
                    .HasMaxLength(8)
                    .HasColumnName("reduction");

                entity.Property(e => e.Titulaire)
                    .HasMaxLength(32)
                    .HasColumnName("titulaire")
                    .IsFixedLength();

                entity.HasOne(d => d.TitulaireNavigation)
                    .WithMany(p => p.Contrats)
                    .HasForeignKey(d => d.Titulaire)
                    .HasConstraintName("FK_CONTRAT_PERSONNE");
            });

            modelBuilder.Entity<Personne>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("PERSONNE");

                entity.Property(e => e.Uid)
                    .HasMaxLength(32)
                    .HasColumnName("uid")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(60)
                    .HasColumnName("prenom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
