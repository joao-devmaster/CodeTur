using CodeTur.Comum.Commands;
using CodeTur.Dominio.Commands.Usuario;
using CodeTur.Dominio.Entidades;
using CodeTur.Dominio.Handlers.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeTur.Api.Controllers
{
    //Properties => Launch => applicationUrl
    //IP SENAI 192.168.5.64

    //IP


    [Route("v1/account")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Cadastra usuarios
        /// </summary>
        /// <param name="command">efetua os comandos</param>
        /// <param name="handler">manipula as classes</param>
        /// <returns></returns>
        [Route("signup")]
        [HttpPost]
        //props( command, [fromservices] manipular/handler)
        public GenericCommandResult SingUp(CriarUsuarioCommand command,[FromServices] CriarUsuarioCommandHandle handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }


        [Route("signin")]
        [HttpPost]
        public GenericCommandResult SignIn(LogarUsuarioCommand command,[FromServices] LogarUsuarioCommandHandle handler)
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            if (resultado.Sucesso)
            {
                var token = GerarJSONWebToken((Usuario)resultado.Data);

                return new GenericCommandResult(resultado.Sucesso, resultado.Mensagem, new { token = token });
            }

            return resultado;
        }

        private string GerarJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyCODETUR"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.TipoUsuario.ToString()),
                new Claim("role", userInfo.TipoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken
                (
                    "codetur",
                    "codetur",
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
