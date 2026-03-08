using System;
using System.ComponentModel.DataAnnotations;
using Liggo.Domain.Enums;

namespace Liggo.Api.Controllers.Operations.Payments.Contracts
{
    public class CreatePaymentRequest
    {
        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public string Concept { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }
    }
}
