using CodeTur.Comum.Queries;
using System;

namespace CodeTur.Dominio.Queries.Pacotes
{
    public class ListarPacotesQuery : IQuery
    {
        public bool? Ativo { get; set; }
        public void Validar()
        {
            
        }
        
    }

    /// <summary>
    /// Retornar somente infos que necessito (pensando em performance)
    /// </summary>
    public class ListarPacotesQueryResult
    {
        public Guid Id { get; set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public string Imagem { get; set; }
        public bool Ativo { get; set; }
        public int QuantidadeComentarios { get; set; }
        public DateTime DataCriacao { get; set; }
    }

}
