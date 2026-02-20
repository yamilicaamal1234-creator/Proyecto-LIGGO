namespace App.Domain.Interfaces;

public interface IFinanceLedgerRepository
{
    // Agrega un nuevo cargo (ej. Mensualidad)
    Task AddChargeAsync(string tenantId, object chargeDocument); 
    
    // Agrega un pago exitoso
    Task AddPaymentAsync(string tenantId, object paymentDocument);
    
    // Lee el historial inmutable para calcular el saldo actual
    Task<decimal> CalculateMemberBalanceAsync(string tenantId, string memberUid);
}