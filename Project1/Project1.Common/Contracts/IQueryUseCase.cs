namespace Project1.Shared.Contracts
{
    public interface IQueryUseCase<TQuery, TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
