using Microsoft.AspNetCore.Mvc;
using Liggo.Infrastructure.Services;

[ApiController]
[Route("api/test")]
public class FirestoreTestController : ControllerBase
{
    private readonly FirestoreService _firestore;

    public FirestoreTestController(FirestoreService firestore)
    {
        _firestore = firestore;
    }

    [HttpPost("firestore")]
    public async Task<IActionResult> TestFirestore()
    {
        var tenantId = Guid.NewGuid().ToString();

        await _firestore.CreateTenantStructureAsync(tenantId, "TenantPrueba");

        return Ok(new { message = "Creado en Firestore", tenantId });
    }
}