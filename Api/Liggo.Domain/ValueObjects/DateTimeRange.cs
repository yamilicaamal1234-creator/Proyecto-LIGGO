namespace Liggo.Domain.ValueObjects
{
    public class DateTimeRange
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("La fecha de fin debe ser mayor a la fecha de inicio");

            Start = start;
            End = end;
        }

        public bool IsOngoing(DateTime currentTime)
        {
            return currentTime >= Start && currentTime <= End;
        }
    }
}