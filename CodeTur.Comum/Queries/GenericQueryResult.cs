using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Comum.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        public static object handle;

        public GenericQueryResult()
        {
        }

        public GenericQueryResult(bool sucesso, string mensagem, object data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }

        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
        public object Data { get; private set; }
    }
}

