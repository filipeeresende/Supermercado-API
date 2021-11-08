using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace supermercado.Models
{
    public partial class SUPERMERCADOContext : DbContext
    {
        public SUPERMERCADOContext()
        {
        }

        public SUPERMERCADOContext(DbContextOptions<SUPERMERCADOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estoque> Estoques { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SUPERMERCADO;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Estoque>(entity =>
            {
                entity.HasKey(e => e.CodEstoque);

                entity.ToTable("ESTOQUE");

                entity.Property(e => e.CodEstoque).HasColumnName("COD_ESTOQUE");

                entity.Property(e => e.CodProduto).HasColumnName("COD_PRODUTO");

                entity.Property(e => e.QuantidadeDisponivel).HasColumnName("QUANTIDADE_DISPONIVEL");

                entity.HasOne(d => d.CodProdutoNavigation)
                    .WithMany(p => p.Estoques)
                    .HasForeignKey(d => d.CodProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ESTOQUE_PRODUTO");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.CodFuncionario)
                    .HasName("PK_dbo_Funcionarios");

                entity.ToTable("FUNCIONARIOS");

                entity.Property(e => e.CodFuncionario).HasColumnName("COD_FUNCIONARIO");

                entity.Property(e => e.Cagor)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CAGOR");

                entity.Property(e => e.NomeFuncionario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOME_FUNCIONARIO");

                entity.Property(e => e.Setor)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SETOR");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.CodProduto);

                entity.ToTable("PRODUTOS");

                entity.Property(e => e.CodProduto).HasColumnName("COD_PRODUTO");

                entity.Property(e => e.NomeProduto)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOME_PRODUTO");

                entity.Property(e => e.Quantidade).HasColumnName("QUANTIDADE");

                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
