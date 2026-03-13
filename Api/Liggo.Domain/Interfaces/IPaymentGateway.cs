namespace Liggo.Domain.Interfaces
{
    public interface IPaymentGateway
    {
        // Genera la orden de cobro (devuelve un link de PayPal o un código de barras de Oxxo)
        Task<string> GeneratePaymentRequestAsync(decimal amount, string currency, string concept, string externalReferenceId);
        
        // Fundamental para los webhooks: Valida la firma de seguridad para asegurar que la señal viene del proveedor y no de un hacker [cite: 114]
        Task<bool> ValidateWebhookSignatureAsync(string payload, string signature);
    }
}