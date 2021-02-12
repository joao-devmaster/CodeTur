using CodeTur.Dominio.Commands.Usuario;
using Xunit;

namespace CodeTur.Testes.Commands
{
    public class CriarUsuarioCommandTestes
    {

        [Fact]
        public void DeveRetornarErrosSeDadosInvalidos()
        {
            var command = new CriarUsuarioCommand();

            //smp validar antes de executar
            command.Validar();

            Assert.False(command.Valid, "Usuário é válido");
        }

        [Fact]
        public void DeveRetornaErrosSeDadosInvalidos()
        {
            var command = new CriarUsuarioCommand("Fernando", "aluno@senai.br", "", "senha1", Comum.Enum.EnTipoUsuario.Admin) ;

            //smp validar antes de executar
            command.Validar();

            Assert.True(command.Valid, "Usuário é inválido");
        }


    }
}
