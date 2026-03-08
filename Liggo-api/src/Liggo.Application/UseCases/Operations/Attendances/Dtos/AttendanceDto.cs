using System;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Attendances.Dtos
{
    public class AttendanceDto
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public AttendanceStatus Status { get; set; }
    }
}
