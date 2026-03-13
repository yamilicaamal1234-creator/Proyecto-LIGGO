namespace Liggo.Domain.Entities.Documents
{
    public class SysUser
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ActiveTenantId { get; set; } = string.Empty;
        public List<string> Tenants { get; set; } = new();
    }
}