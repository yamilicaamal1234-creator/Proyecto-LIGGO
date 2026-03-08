using System;
using System.ComponentModel.DataAnnotations;

namespace liggo_blazor.Models
{
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Overdue,
        Canceled
    }

    public class CreatePaymentRequest
    {
        [Required(ErrorMessage = "Debe seleccionar un jugador.")]
        public Guid? PlayerId { get; set; }

        [Required(ErrorMessage = "El concepto es obligatorio.")]
        public string Concept { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime? Date { get; set; } = DateTime.Today;

        [Required]
        public PaymentStatus Status { get; set; }
    }
}
