using Liggo.Infrastructure.Persistence.Firebase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class SchoolRepository : ISchoolRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "schools";

    public SchoolRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<School?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var docRef = _db.Collection(CollectionName).Document(id);
        var snapshot = await docRef.GetSnapshotAsync(cancellationToken);

        if (!snapshot.Exists) return null;

        // Mapeo manual desde el Documento de Firestore a nuestra entidad pura de Dominio
        var data = snapshot.ToDictionary();
        var info = data.ContainsKey("Info") ? (Dictionary<string, object>)data["Info"] : new Dictionary<string, object>();
        var settings = data.ContainsKey("Settings") ? (Dictionary<string, object>)data["Settings"] : new Dictionary<string, object>();
        
        var categoriesList = settings.ContainsKey("Categories") 
            ? (List<object>)settings["Categories"] 
            : new List<object>();

        return new School
        {
            Id = snapshot.Id,
            Info = new SchoolInfo
            {
                Name = info.ContainsKey("Name") ? info["Name"].ToString() ?? "" : "",
                Plan = info.ContainsKey("Plan") ? info["Plan"].ToString() ?? "" : "",
                LogoUrl = info.ContainsKey("LogoUrl") ? info["LogoUrl"].ToString() ?? "" : ""
            },
            Settings = new SchoolSettings
            {
                Currency = settings.ContainsKey("Currency") ? settings["Currency"].ToString() ?? "MXN" : "MXN",
                Timezone = settings.ContainsKey("Timezone") ? settings["Timezone"].ToString() ?? "America/Mexico_City" : "America/Mexico_City",
                Categories = categoriesList.Select(c => 
                {
                    var catDict = (Dictionary<string, object>)c;
                    var yearsList = catDict.ContainsKey("Years") ? (List<object>)catDict["Years"] : new List<object>();
                    return new CategoryInfo
                    {
                        Name = catDict.ContainsKey("Name") ? catDict["Name"].ToString() ?? "" : "",
                        Years = yearsList.Select(y => int.Parse(y.ToString()!)).ToList()
                    };
                }).ToList()
            }
        };
    }

    public async Task<IEnumerable<School>> GetAllByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default)
    {
        // Consulta a Firebase filtrando por TenantId
        var query = _db.Collection(CollectionName).WhereEqualTo("TenantId", tenantId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents.Select(d => 
        {
            var doc = d.ConvertTo<SchoolDocument>();
            return new School
            {
                Id = d.Id,
                Info = new SchoolInfo
                {
                    Name = doc.Info?.Name ?? string.Empty,
                    Plan = doc.Info?.Plan ?? string.Empty,
                    LogoUrl = doc.Info?.LogoUrl ?? string.Empty
                },
                Settings = new SchoolSettings
                {
                    Currency = doc.Settings?.Currency ?? "MXN",
                    Timezone = doc.Settings?.Timezone ?? "America/Mexico_City",
                    Categories = doc.Settings?.Categories?.Select(c => new CategoryInfo
                    {
                        Name = c.Name ?? string.Empty,
                        Years = c.Years ?? new System.Collections.Generic.List<int>()
                    }).ToList() ?? new System.Collections.Generic.List<CategoryInfo>()
                }
            };
        }).ToList();
    }

    public async Task AddAsync(School school, CancellationToken cancellationToken = default)
    {
        // Mapeamos nuestra entidad de dominio a un Diccionario para Firestore
        var docData = MapToDictionary(school);
        
        var docRef = _db.Collection(CollectionName).Document(school.Id);
        await docRef.SetAsync(docData, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(School school, CancellationToken cancellationToken = default)
    {
        var docData = MapToDictionary(school);
        
        var docRef = _db.Collection(CollectionName).Document(school.Id);
        await docRef.SetAsync(docData, SetOptions.MergeAll, cancellationToken); // MergeAll actualiza sin borrar campos extra
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var docRef = _db.Collection(CollectionName).Document(id);
        await docRef.DeleteAsync(cancellationToken: cancellationToken);
    }

    // Método Helper privado para convertir la entidad en un Diccionario compatible con Firestore
    private Dictionary<string, object> MapToDictionary(School school)
    {
        return new Dictionary<string, object>
        {
            { "Id", school.Id },
            { "Info", new Dictionary<string, object>
                {
                    { "Name", school.Info.Name },
                    { "Plan", school.Info.Plan },
                    { "LogoUrl", school.Info.LogoUrl }
                }
            },
            { "Settings", new Dictionary<string, object>
                {
                    { "Currency", school.Settings.Currency },
                    { "Timezone", school.Settings.Timezone },
                    { "Categories", school.Settings.Categories.Select(c => new Dictionary<string, object>
                        {
                            { "Name", c.Name },
                            { "Years", c.Years }
                        }).ToList()
                    }
                }
            }
        };
    }
}