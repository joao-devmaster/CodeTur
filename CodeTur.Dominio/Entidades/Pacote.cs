using CodeTur.Comum.Entidades;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace CodeTur.Dominio.Entidades
{
    public class Pacote : Entidade
    {
        //deixando visivel os comentarios somente nessa classe
        private readonly List<Comentario> _comentarios;

        //Gerando construtor com as props necessárias
        /// <summary>
        /// Construtor padrão para gerar novo pacote
        /// </summary>
        /// <param name="titulo">Titulo do pacote</param>
        /// <param name="descricao">Descrição do pacote</param>
        /// <param name="imagem">Imagem referente ao pacote</param>
        /// <param name="ativo">visibilidade do pacote</param>
        public Pacote(string titulo, string descricao, string imagem, bool ativo)
        {
            //Contrato de criação
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(titulo, 3, "Titulo", "O nome deve ter pelo menos 3 caracteres")
                .HasMaxLen(titulo, 40, "Titulo", "O nome deve ter no máximo 40 caracteres")
                .IsNotNullOrEmpty(descricao, "Descricao", "Informe a descrição do pacote")
                .IsNotNullOrEmpty(imagem, "Imagem", "Informe a imagem do pacote")
            );

            if (Valid)
            {
            Titulo = titulo;
            Descricao = descricao;
            Imagem = imagem;
            Ativo = ativo;
            _comentarios = new List<Comentario>();
            }
        }

        //propriedades da classe
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public string Imagem { get; set; }

        public bool Ativo { get; set; }

        //como n vamos trabalhar com set, fazer return
        public IReadOnlyCollection<Comentario> Comentarios { get { return _comentarios.ToArray(); } }

        /// <summary>
        /// Usuario adiciona comentário ao pacote  
        /// </summary>
        /// <param name="comentario">texto</param>
        public void AdicionarComentario(Comentario comentario)
        {
            //verifiicar se usuario ja tem um comentario no pacote
            //evitar spam
            if (_comentarios.Any(x => x.IdUsuario == comentario.IdUsuario))
                AddNotification("Comentarios", "Usuário já possui um comentário neste pacote");

            if (Valid)
                _comentarios.Add(comentario);
        }

        /// <summary>
        /// Habilita a visibilidade do Pacote (regra de negócio)
        /// </summary>
        public void AtivarPacote()
        {
            Ativo = true;
        }

        /// <summary>
        /// Desabilita a visibilidade do Pacote (regra de negócio)
        /// </summary>
        public void DesativarPacote()
        {
            Ativo = false;
        }

        /// <summary>
        /// Altera as infos presentes no pacote
        /// </summary>
        /// <param name="titulo">Titulo do pacote</param>
        /// <param name="descricao">Descrição do pacote</param>
        public void AtualizaPacote(string titulo, string descricao)
        {
            //Contrato para Atualização do pacote
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(titulo, "Titulo", "Informe o Título do pacote")
                .IsNotNullOrEmpty(descricao, "Descricao", "Informe o Descrição do pacote")
            );

            //se for válido, ALTERA:
            if (Valid)
            {
                Titulo = titulo;
                Descricao = descricao;
            }
        }

        /// <summary>
        /// Altera a imagem presente no pacote
        /// </summary>
        /// <param name="imagem">Imagem referente ao pacote</param>
        public void AtualizaImagem(string imagem)
        {
            //Contrato para atualização do pacote
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(imagem, "Imagem", "Informe a imagem do pacote")
            );

            //se for válido, ALTERA:
            if (Valid)
                Imagem = imagem;
        }
    }
}