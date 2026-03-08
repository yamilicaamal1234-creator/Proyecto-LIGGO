using System;
using System.ComponentModel.DataAnnotations;

namespace liggo_blazor.Models
{
    // Using an enum for the plan for better type safety in the UI
    public enum PaymentPlan
    {
        Anual,
        Semestral
    }

    public class CreateRegistrationRequest
    {
        [Required(ErrorMessage = "Debe seleccionar un jugador.")]
        public Guid? PlayerId { get; set; }

        [Required]
        public PaymentPlan Plan { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime? StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime? EndDate { get; set; } = DateTime.Today.AddYears(1);

        [Required]
        [Range(0.01, 99999.99, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Amount { get; set; }

        // The status will likely be set to Active by default on creation
        // So we don't need it in the creation form.
    }
}
