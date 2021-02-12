using CodeTur.Comum.Handlers;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Queries.Pacotes;
using CodeTur.Dominio.Repositorios;
using System.Linq;

namespace CodeTur.Dominio.Handlers.Pacotes
{
    public class ListarPacotesQueryHandle : IHandlerQuery<ListarPacotesQuery>
    {
        private readonly IPacoteRepositorio _pacoteRepositorio;

        //Injeção de Dependência
        public ListarPacotesQueryHandle(IPacoteRepositorio pacoteRepositorio)
        {
            _pacoteRepositorio = pacoteRepositorio;
        }

        public IQueryResult Handle(ListarPacotesQuery query)
        {
            var pacotes = _pacoteRepositorio.Listar(query.Ativo);

            //Linq
            var retornoPacotes = pacotes.Select(
                d =>
                {
                    return new ListarPacotesQueryResult
                    {
                        Id = d.Id,
                        Titulo = d.Titulo,
                        Descricao = d.Descricao,
                        Ativo = d.Ativo,
                        QuantidadeComentarios = d.Comentarios.Count(),
                        DataCriacao = d.DataCriacao
                    };
                }
            );

            return new GenericQueryResult(true, "Lista de Pacotes", retornoPacotes);
        }
    }
}
