using CodeTur.Comum.Commands;

namespace CodeTur.Comum.Handlers
{
    //Manipular = Handler
    //Handler só herda o objeto anonimo <T> onde esse obj anonimo herde do ICommand
    //Escadinha analogia (um herda do outro, que herda de outro)
    public interface IHandlerCommand<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
