using CodeTur.Comum.Queries;

namespace CodeTur.Comum.Handlers
{
    public interface IHandlerQuery<T> where T : IQuery
    {
        IQueryResult Handle(T query);
    }
}
