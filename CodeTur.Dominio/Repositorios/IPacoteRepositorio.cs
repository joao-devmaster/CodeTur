using CodeTur.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace CodeTur.Dominio.Repositorios
{
    public interface IPacoteRepositorio
    {
        void Adicionar(Pacote pacote);

        void Alterar(Pacote pacote);

        //? na frente da var permite que ela seja nula
        //IEnumerable é uma interface que marca as classes que desejam implementá-la para que se saiba que ela possa ser iterável através de um iterador
        IEnumerable<Pacote> Listar(bool? ativo = null);

        Pacote BuscarPorTitulo(string titulo);

        Pacote BuscarPorId(Guid id);

    }
}
