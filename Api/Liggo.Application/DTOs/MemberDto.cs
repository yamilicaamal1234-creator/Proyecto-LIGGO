using System.Security.Cryptography.X509Certificates;

namespace Liggo.Application.DTOs
{
    public class MemberDto
    {

        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Uid { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public float WalletBalance { get; set; } 
        public string WalletStatus { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}