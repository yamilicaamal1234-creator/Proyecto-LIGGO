using System;
using System.ComponentModel.DataAnnotations;

namespace liggo_blazor.Models
{
    public class CreatePlayerRequest
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime? DateOfBirth { get; set; } = DateTime.Today;

        public string AssignedTeam { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del tutor es obligatorio.")]
        public string GuardianName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono del tutor es obligatorio.")]
        public string GuardianPhone { get; set; } = string.Empty;

        public string Relationship { get; set; } = string.Empty;
    }
}
