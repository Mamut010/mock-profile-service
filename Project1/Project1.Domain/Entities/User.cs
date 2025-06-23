namespace Project1.Domain.Entities
{
    public class User
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public string? DisplayedName { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public string? Email { get; set; }
    }
}
