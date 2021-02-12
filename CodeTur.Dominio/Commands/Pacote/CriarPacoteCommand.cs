using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CodeTur.Dominio.Commands.Pacote
{
    //Command = metodos POST/PUT/DELETE
    public class CriarPacoteCommand : Notifiable, ICommand
    {

        //Construtor vazio
        public CriarPacoteCommand()
        {

        }

        /// <summary>
        /// Construtor padrão para criar um novo pacote
        /// </summary>
        /// <param name="titulo">Titulo do pacote</param>
        /// <param name="descricao">Descrição do pacote</param>
        /// <param name="imagem">Imagem referente ao pacote</param>
        /// <param name="ativo">Visibilidade do pacote</param>
        public CriarPacoteCommand(string titulo, string descricao, string imagem, bool ativo)
        {
            Titulo = titulo;
            Descricao = descricao;
            Imagem = imagem;
            Ativo = ativo;
        }

        //propriedades da classe
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public bool Ativo { get; set; }

        /// <summary>
        /// Valida se as entradas seguem as exigências do contrato
        /// </summary>
        public void Validar()
        {
            //Contrato + exigências pra criação de um novo pacote
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Titulo, "Titulo", "Informe o Título do pacote")
                .IsNotNullOrEmpty(Descricao, "Descricao", "Informe o Descrição do pacote")
                .IsNotNullOrEmpty(Imagem, "Imagem", "Informe o Imagem do pacote")
            );
        }
    }
}
