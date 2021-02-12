using CodeTur.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTur.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        void Adicionar(Usuario usuario);

        //Alterar senha tbm segue o msm principio
        void Alterar(Usuario usuario);

        Usuario BuscarPorEmail(string email);

        Usuario BuscarPorId(Guid id);

        //somente pacotes habilitados
        ICollection<Usuario> Listar();
    }
}
