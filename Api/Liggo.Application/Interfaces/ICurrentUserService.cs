namespace Liggo.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; } 
        string SchoolId { get; } 
        bool IsSuperAdmin { get; } 
    }
}