using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers;
using CodeTur.Comum.Utils;
using CodeTur.Dominio.Commands.Usuario;
using CodeTur.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Dominio.Handlers.Usuarios
{
    public class LogarUsuarioCommandHandle : IHandlerCommand<LogarUsuarioCommand>
    {
        //Injeção de Dependência
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        //construtor da classe
        public LogarUsuarioCommandHandle(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ICommandResult Handle(LogarUsuarioCommand command)
        {
            //Valida Command
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados incorretos", command.Notifications);

            //Verifica se usuario existe
            var usuario = _usuarioRepositorio.BuscarPorEmail(command.Email);

            if (usuario == null)
                return new GenericCommandResult(false, "Email inválido", null);

            //Verifica se senha é válida
            if (!Senha.Validar(command.Senha, usuario.Senha));

            //retorna true + dados do user
            return new GenericCommandResult(true, "Usuário Logado", usuario);



        }

    }
}
