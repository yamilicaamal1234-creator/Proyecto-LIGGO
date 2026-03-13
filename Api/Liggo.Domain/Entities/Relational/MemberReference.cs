namespace Liggo.Domain.Entities.Relational
{
    public class MemberReference
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirebaseUid { get; set; } = string.Empty;
        public Guid SchoolId { get; set; }
        public string ExternalName { get; set; } = string.Empty;
    }
}