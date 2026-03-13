namespace Liggo.Domain.Entities.Relational
{
    public class User
    {
        public string FirebaseUid { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid SchoolId { get; set; } 

        public bool IsSuperAdmin { get; set; } = false;
    }
}