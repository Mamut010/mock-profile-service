namespace Project1.Infrastructure.Persistence.Models
{
    public class UserModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public string? DisplayedName { get; set; }

        public string? Email { get; set; }
    }
}
