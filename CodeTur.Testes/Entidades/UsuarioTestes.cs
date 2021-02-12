using CodeTur.Comum.Enum;
using CodeTur.Dominio.Entidades;
using Xunit;

namespace CodeTur.Testes.Entidades
{
    public class UsuarioTestes
    {
        [Fact]
        public void DeveRetornarErroSeUsuarioForInvalido()
        {
            var usuario = new Usuario("", "", "", EnTipoUsuario.Admin );
            //testando com Assert (xUnit)
            Assert.True(usuario.Invalid, "Usuario é valido");
        }

        [Fact]
        public void DeveRetornarSucessoSeUsuarioForValido()
        {
            var usuario = new Usuario("Fernando Araujo", "fernandoa@senai.br", "senha1", EnTipoUsuario.Admin);
            usuario.AdicionarTelefone("(11)921984021");

            //testando com Assert (xUnit)
            Assert.True(usuario.Valid, "Usuario é invalido");
        }

        //lógica simples
        [Fact]
        public void DeveRetornarErroSeTelefoneForInvalido()
        {
            //numero incompleto
            var usuario = new Usuario("Fernando Araujo", "fernandoa@senai.br", "senha1", EnTipoUsuario.Admin);
            usuario.AdicionarTelefone("11921984");

            //testando com Assert (xUnit)
            Assert.True(usuario.Invalid, "Numero do telefone é invalido");
        }

        [Fact]
        public void DeveRetornarSucessoSeTelefoneForValido()
        {
            //numero COMPLETO
            var usuario = new Usuario("Fernando Araujo", "fernandoa@senai.br", "senha1", EnTipoUsuario.Admin);
            usuario.AdicionarTelefone("11921984387");

            //testando com Assert (xUnit)
            Assert.True(usuario.Valid, "Numero do telefone é valido");
        }

    }
}
