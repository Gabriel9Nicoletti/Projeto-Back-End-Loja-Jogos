using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using jogos_api.Models;

namespace jogos_api.Data;

public partial class LojaJogosContext : DbContext
{
    public LojaJogosContext(DbContextOptions<LojaJogosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<ItensCompra> ItensCompras { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.ClienteEmail, "cliente_email").IsUnique();

            entity.HasIndex(e => e.ClienteNomeUsuario, "cliente_nome_usuario").IsUnique();

            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.ClienteDataCadastro)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("cliente_data_cadastro");
            entity.Property(e => e.ClienteEmail)
                .HasMaxLength(150)
                .HasColumnName("cliente_email");
            entity.Property(e => e.ClienteNome)
                .HasMaxLength(120)
                .HasColumnName("cliente_nome");
            entity.Property(e => e.ClienteNomeUsuario)
                .HasMaxLength(50)
                .HasColumnName("cliente_nome_usuario");
            entity.Property(e => e.ClienteSenha)
                .HasMaxLength(255)
                .HasColumnName("cliente_senha");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.CompraId).HasName("PRIMARY");

            entity.ToTable("compras");

            entity.HasIndex(e => e.ClienteId, "cliente_id");

            entity.Property(e => e.CompraId)
                .HasColumnType("int(11)")
                .HasColumnName("compra_id");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.CompraData)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("compra_data");
            entity.Property(e => e.CompraSituacao)
                .HasDefaultValueSql("'''pendente'''")
                .HasColumnType("enum('pendente','aprovada','cancelada')")
                .HasColumnName("compra_situacao");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Compras)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("compras_ibfk_1");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PRIMARY");

            entity.ToTable("generos");

            entity.HasIndex(e => e.GeneroNome, "genero_nome").IsUnique();

            entity.Property(e => e.GeneroId)
                .HasColumnType("int(11)")
                .HasColumnName("genero_id");
            entity.Property(e => e.GeneroDescricao)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("genero_descricao");
            entity.Property(e => e.GeneroNome)
                .HasMaxLength(80)
                .HasColumnName("genero_nome");
        });

        modelBuilder.Entity<ItensCompra>(entity =>
        {
            entity.HasKey(e => e.ItemCompraId).HasName("PRIMARY");

            entity.ToTable("itens_compras");

            entity.HasIndex(e => new { e.CompraId, e.JogoId }, "compra_id").IsUnique();

            entity.HasIndex(e => e.JogoId, "jogo_id");

            entity.Property(e => e.ItemCompraId)
                .HasColumnType("int(11)")
                .HasColumnName("item_compra_id");
            entity.Property(e => e.CompraId)
                .HasColumnType("int(11)")
                .HasColumnName("compra_id");
            entity.Property(e => e.ItemCompraPreco)
                .HasPrecision(10)
                .HasColumnName("item_compra_preco");
            entity.Property(e => e.JogoId)
                .HasColumnType("int(11)")
                .HasColumnName("jogo_id");

            entity.HasOne(d => d.Compra).WithMany(p => p.ItensCompras)
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("itens_compras_ibfk_1");

            entity.HasOne(d => d.Jogo).WithMany(p => p.ItensCompras)
                .HasForeignKey(d => d.JogoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("itens_compras_ibfk_2");
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoId).HasName("PRIMARY");

            entity.ToTable("jogos");

            entity.HasIndex(e => e.GeneroId, "genero_id");

            entity.Property(e => e.JogoId)
                .HasColumnType("int(11)")
                .HasColumnName("jogo_id");
            entity.Property(e => e.GeneroId)
                .HasColumnType("int(11)")
                .HasColumnName("genero_id");
            entity.Property(e => e.JogoClassificacaoIndicativa)
                .HasColumnType("enum('Livre','10','12','14','16','18')")
                .HasColumnName("jogo_classificacao_indicativa");
            entity.Property(e => e.JogoDataLancamento)
                .HasColumnType("date")
                .HasColumnName("jogo_data_lancamento");
            entity.Property(e => e.JogoDescricao)
                .HasMaxLength(500)
                .HasColumnName("jogo_descricao");
            entity.Property(e => e.JogoDesenvolvedora)
                .HasMaxLength(120)
                .HasColumnName("jogo_desenvolvedora");
            entity.Property(e => e.JogoPreco)
                .HasPrecision(10)
                .HasColumnName("jogo_preco");
            entity.Property(e => e.JogoTitulo)
                .HasMaxLength(150)
                .HasColumnName("jogo_titulo");

            entity.HasOne(d => d.Genero).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.GeneroId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("jogos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
