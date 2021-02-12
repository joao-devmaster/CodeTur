using CodeTur.Dominio.Entidades;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Infra.Data.Contexts
{
    //Modelando DBO com EntityFrameworkCore e FluentAPI
    public class CodeTurContext : DbContext
    {
        //contrutor para passar o options do CodeTur e na base é o DbContext
        //relação de escada
        public CodeTurContext(DbContextOptions<CodeTurContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //Definindo tabelas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();


            #region Mapeamento Tabela Usuarios
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().Property(x => x.Id);

            //Propriedades

            //Nome
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasMaxLength(40);
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasColumnType("varchar(40)");
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).IsRequired();
            
            //Email
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasMaxLength(60);
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasColumnType("varchar(60)");
            modelBuilder.Entity<Usuario>().Property(x => x.Email).IsRequired();
            
            //Senha
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasMaxLength(60);
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasColumnType("varchar(60)");
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired();
            
            //Telefone
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasMaxLength(11);
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasColumnType("varchar(11)");

            //Comentarios
            //Relacionamento entre tabelas - 1 pra mtos
            //https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
            modelBuilder.Entity<Usuario>()
                        .HasMany(c => c.Comentarios)
                        .WithOne(u => u.Usuario)
                        .HasForeignKey(fk => fk.IdUsuario);
            
            //Datas
            modelBuilder.Entity<Usuario>().Property(x => x.DataCriacao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(x => x.DataAlteracao).HasColumnType("DateTime");
            #endregion

            //TODO
            #region Mapeamento Tabela Comentarios
            modelBuilder.Entity<Comentario>().ToTable("Comentarios");
            //Defini como chave primaria
            modelBuilder.Entity<Comentario>().Property(x => x.Id);
            //Texto
            modelBuilder.Entity<Comentario>().Property(x => x.Texto).HasMaxLength(500);
            modelBuilder.Entity<Comentario>().Property(x => x.Texto).HasColumnType("varchar(500)");
            modelBuilder.Entity<Comentario>().Property(x => x.Texto).IsRequired();
            //Sentimento
            modelBuilder.Entity<Comentario>().Property(x => x.Sentimento).HasMaxLength(40);
            modelBuilder.Entity<Comentario>().Property(x => x.Sentimento).HasColumnType("varchar(40)");
            modelBuilder.Entity<Comentario>().Property(x => x.Sentimento).IsRequired();
            //Status
            modelBuilder.Entity<Comentario>().Property(x => x.Status).HasColumnType("int");

            //DataCriacao
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasDefaultValueSql("GetDate()");
            //DataAlteracao
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasDefaultValueSql("GetDate()");
            #endregion

            //TODO
            #region Mapeamento Tabela Pacotes
            modelBuilder.Entity<Pacote>().ToTable("Pacotes");

            //Defini como chave primaria
            modelBuilder.Entity<Pacote>().Property(x => x.Id);

            //Titulo
            modelBuilder.Entity<Pacote>().Property(x => x.Titulo).HasMaxLength(120);
            modelBuilder.Entity<Pacote>().Property(x => x.Titulo).HasColumnType("varchar(120)");
            modelBuilder.Entity<Pacote>().Property(x => x.Titulo).IsRequired();

            //Descrição
            modelBuilder.Entity<Pacote>().Property(x => x.Descricao).HasColumnType("Text");
            modelBuilder.Entity<Pacote>().Property(x => x.Descricao).IsRequired();

            //Imagem
            modelBuilder.Entity<Pacote>().Property(x => x.Imagem).HasMaxLength(250);
            modelBuilder.Entity<Pacote>().Property(x => x.Imagem).HasColumnType("varchar(250)");
            modelBuilder.Entity<Pacote>().Property(x => x.Imagem).IsRequired();

            //Ativo
            //bit = bool no SQL SERVER
            //0 = false / 1 = true / null
            modelBuilder.Entity<Pacote>().Property(x => x.Ativo).HasColumnType("bit");

            //Comentarios
            //Relacionamento entre tabelas - 1 pra mtos
            //https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
            modelBuilder.Entity<Pacote>()
                .HasMany(c => c.Comentarios)
                .WithOne(e => e.Pacote)
                .HasForeignKey(x => x.IdPacote);

            //DataCriacao
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasDefaultValueSql("GetDate()");

            //DataAlteracao
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasDefaultValueSql("GetDate()");

            #endregion


            base.OnModelCreating(modelBuilder);
        }

    }
}
