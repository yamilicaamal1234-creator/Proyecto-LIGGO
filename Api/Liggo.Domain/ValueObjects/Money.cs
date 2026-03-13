namespace Liggo.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("El monto no puede ser negativo");

            Amount = amount;
            Currency = currency.ToUpper();
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("No puedes sumar monedas diferentes");

            return new Money(Amount + other.Amount, Currency);
        }
    }
}