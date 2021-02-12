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
    public class PacoteRepositorio : IPacoteRepositorio
    {
        //Preciso do meu contexto 
        private readonly CodeTurContext _context;

        //Injeção de Dependência
        public PacoteRepositorio(CodeTurContext context)
        {
            _context = context;
        }

        public void Adicionar(Pacote pacote)
        {
            _context.Pacotes.Add(pacote);
            _context.SaveChanges();

        }

        public void Alterar(Pacote pacote)
        {
            //Entry = permite a alteração
            _context.Entry(pacote).State = EntityState.Modified;

        }

        public Pacote BuscarPorId(Guid id)
        {
            return _context
                .Pacotes
                //AsNoTracking significa que as entidades serão lidas da origem de dados mas não serão mantidas no contexto.
                .AsNoTracking()
                //Inner Join para visualizar Comentários
                .Include(h => h.Comentarios)
                .FirstOrDefault(o => o.Id == id);
        }

        public Pacote BuscarPorTitulo(string titulo)
        {
            return _context
                .Pacotes
                //AsNoTracking significa que as entidades serão lidas da origem de dados mas não serão mantidas no contexto.
                .AsNoTracking()
                //Inner Join para visualizar Comentários
                .Include(h => h.Comentarios)
                //ToLower deixar todos com letras minusculas
                .FirstOrDefault(o => o.Titulo.ToLower() == titulo.ToLower());
        }


        public IEnumerable<Pacote> Listar(bool? ativo = null)
        {
            //Condição para visibilidade do pacote (regra de negócio)
            if (ativo == null)
            {
                return _context
                    .Pacotes
                    .AsNoTracking()
                    .Include(f => f.Comentarios)
                    .OrderBy(o => o.DataCriacao);
            }

            else
            {
                return _context
                    .Pacotes
                    .AsNoTracking()
                    .Include(f => f.Comentarios)
                    .Where(o => o.Ativo == ativo)
                    .OrderBy(o => o.DataCriacao);
            }


        }


    }
}
