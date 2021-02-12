using CodeTur.Comum.Commands;
using CodeTur.Comum.Enum;
using System;
using Flunt.Validations;
using Flunt.Notifications;

namespace CodeTur.Dominio.Commands.Usuario
{
    //TRABALHANDO com CQRS
    //Command = Somente as prop de entrada e as validações, sem nenhum método
    public class CriarUsuarioCommand : Notifiable, ICommand
    {
        
        //Construtor vazio para testes
        public CriarUsuarioCommand()
        {

        }

        public CriarUsuarioCommand(string nome, string email, string telefone, string senha, EnTipoUsuario tipoUsuario)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Senha = senha;
            TipoUsuario = tipoUsuario;
        }

        public string Nome { get;  set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }
        public string Senha { get;  set; }
        public EnTipoUsuario TipoUsuario { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Nome, 3, "Nome", "O nome deve ter pelo menos 3 caracteres")
                .HasMaxLen(Nome, 40, "Nome", "O nome deve ter no máximo 40 caracteres")
                .IsEmail(Email, "Email", "Informe um e-mail válido")
                .HasMinLen(Senha, 6, "Senha", "A senha deve conter pelo menos 6 caracteres")
                .HasMaxLen(Senha, 12, "Senha", "A senha deve conter no maximo 12 caracteres")
            );
        }
    }
}
