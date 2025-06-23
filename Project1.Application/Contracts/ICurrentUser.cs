namespace Project1.Application.Contracts
{
    public interface ICurrentUser
    {
        int? UserId { get; }

        bool IsAuthenticated { get; }
    }
}
