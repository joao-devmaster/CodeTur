using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Comum.Entidades
{
    public class Entidade : Notifiable
    {

        //Metodo construtor para instanciar assim que iniciar
        public Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            DataAlteracao = DateTime.Now;
        }

        public DateTime DataCriacao { get; private set; }
        public DateTime DataAlteracao { get; private set; }
        public Guid Id { get; private set; }

    }
}
