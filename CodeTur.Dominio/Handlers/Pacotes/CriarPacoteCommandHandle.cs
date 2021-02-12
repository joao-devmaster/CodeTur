using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers;
using CodeTur.Dominio.Commands.Pacote;
using CodeTur.Dominio.Entidades;
using CodeTur.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Dominio.Handlers.Pacotes
{
    public class CriarPacoteCommandHandle : IHandlerCommand<CriarPacoteCommand>
    {
        private readonly IPacoteRepositorio _pacoteRepositorio;

        public CriarPacoteCommandHandle(IPacoteRepositorio pacoteRepositorio)
        {
            _pacoteRepositorio = pacoteRepositorio;
        }

        public ICommandResult Handle(CriarPacoteCommand command)
        {
            //Validação
            command.Validar();

            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Dados incorretos", command.Notifications);
            }

            //Verfica se existe um pacote com o mesmo titulo
            var pacoteExiste = _pacoteRepositorio.BuscarPorTitulo(command.Titulo);

            if (pacoteExiste != null)
            {
                return new GenericCommandResult(false, "Titulo do pacote já cadastrado", null);
            }

            //Gera entidade pacote
            var pacote = new Pacote(command.Titulo, command.Descricao, command.Imagem, command.Ativo);

            if (pacote.Invalid)
            {
                return new GenericCommandResult(false, "Dados inválidos", pacote.Notifications);
            }

            //Adiciona no banco
            _pacoteRepositorio.Adicionar(pacote);

            //retorna mensagem de sucesso
            return new GenericCommandResult(true, "Pacote Craido", null);
        }
    }
}
