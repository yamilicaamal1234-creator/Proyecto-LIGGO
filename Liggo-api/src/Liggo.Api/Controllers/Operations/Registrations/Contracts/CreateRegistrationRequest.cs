using System;
using System.ComponentModel.DataAnnotations;
using Liggo.Domain.Enums;

namespace Liggo.Api.Controllers.Operations.Registrations.Contracts
{
    public class CreateRegistrationRequest
    {
        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public PaymentPlan Plan { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal Amount { get; set; }

        [Required]
        public RegistrationStatus Status { get; set; }
    }
}
