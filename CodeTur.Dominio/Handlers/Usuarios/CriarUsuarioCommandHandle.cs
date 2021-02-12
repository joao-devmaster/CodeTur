using CodeTur.Comum.Handlers;
using CodeTur.Comum.Commands;
using CodeTur.Dominio.Commands.Usuario;
using Flunt.Notifications;
using CodeTur.Dominio.Repositorios;
using CodeTur.Comum.Utils;
using CodeTur.Dominio.Entidades;

namespace CodeTur.Dominio.Handlers.Usuarios
{
    public class CriarUsuarioCommandHandle : Notifiable, IHandlerCommand<CriarUsuarioCommand>
    {

        //Injeção de dependência
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CriarUsuarioCommandHandle(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ICommandResult Handle(CriarUsuarioCommand command)
        {
            //Valida Command
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados incorretos", command.Notifications);

            //Verifica se email já existe
            //Fake Repo
            var usuarioExiste = _usuarioRepositorio.BuscarPorEmail(command.Email);

            if(usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado", null);

            //Criptografa a senha 
            command.Senha = Senha.Criptografar(command.Senha);

            //Salva Usuario
            var usuario = new Usuario(command.Nome, command.Email, command.Senha, command.TipoUsuario);

            if (!string.IsNullOrEmpty(command.Telefone))
                usuario.AdicionarTelefone(command.Telefone);

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Usuário invalido", usuario.Notifications);

            _usuarioRepositorio.Adicionar(usuario);

            return new GenericCommandResult(true, "Usuario criado", "token");
        }

     
    }
}
