using CodeTur.Dominio.Entidades;
using CodeTur.Dominio.Repositorios;
using CodeTur.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTur.Infra.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly CodeTurContext _context;
        
        //Trabalhando com Injection Dependency
        public UsuarioRepositorio(CodeTurContext context)
        {
            _context = context;
        }


        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Alterar(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Usuario BuscarPorEmail(string email)
        {
            //Usando Expressão lambda
            //ToLower() = Retorna uma cópia dessa cadeia de caracteres convertida em minúsculas
            return _context.Usuarios.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
        }

        public Usuario BuscarPorId(Guid id)
        {
            //Usando Expressão lambda
            //ToLower() = Retorna uma cópia dessa cadeia de caracteres convertida em minúsculas
            return _context.Usuarios.FirstOrDefault(e => e.Id == id);
        }

        public ICollection<Usuario> Listar()
        {

            return _context.Usuarios
                //AsNoTracking significa que as entidades serão lidas da origem de dados mas não serão mantidas no contexto.
                .AsNoTracking()
                //Incluindo comentarios no Contexto
                .Include(i => i.Comentarios)
                .OrderBy(o => o.DataCriacao)
                .ToList();
        }
    }
}
