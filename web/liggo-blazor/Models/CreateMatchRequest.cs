using System;
using System.ComponentModel.DataAnnotations;

namespace liggo_blazor.Models
{
    public enum MatchCategory
    {
        Sub9,
        Sub11,
        Sub13,
        Sub15,
        Sub17
    }

    public class CreateMatchRequest
    {
        [Required(ErrorMessage = "El equipo local es obligatorio.")]
        public string LocalTeam { get; set; } = string.Empty;

        [Required(ErrorMessage = "El equipo visitante es obligatorio.")]
        public string VisitingTeam { get; set; } = string.Empty;

        [Required]
        public DateTime? DateTime { get; set; } = System.DateTime.Now;

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public string Location { get; set; } = string.Empty;

        [Required]
        public MatchCategory Category { get; set; }
    }
}
