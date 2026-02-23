using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.Firebase.Models;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class LedgerTransactionRepository : ILedgerTransactionRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "ledger_transactions";

    public LedgerTransactionRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<LedgerTransaction?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync(cancellationToken);
        if (!snapshot.Exists) return null;

        var doc = snapshot.ConvertTo<LedgerTransactionDocument>();
        return MapToDomain(doc, snapshot.Id);
    }

    public async Task<IEnumerable<LedgerTransaction>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
    {
        var query = _db.Collection(CollectionName).WhereEqualTo("SchoolId", schoolId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<LedgerTransactionDocument>(), d.Id))
            .ToList();
    }

    public async Task<IEnumerable<LedgerTransaction>> GetAllByMemberIdAsync(string memberId, CancellationToken cancellationToken = default)
    {
        var query = _db.Collection(CollectionName).WhereEqualTo("MemberId", memberId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<LedgerTransactionDocument>(), d.Id))
            .ToList();
    }

    public async Task AddAsync(LedgerTransaction transaction, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(transaction);
        await _db.Collection(CollectionName).Document(transaction.Id).SetAsync(doc, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(LedgerTransaction transaction, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(transaction);
        // MergeAll para conservar los campos SchoolId y MemberId que no existen en el Dominio puro
        await _db.Collection(CollectionName).Document(transaction.Id).SetAsync(doc, SetOptions.MergeAll, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync(cancellationToken: cancellationToken);
    }

    // ==========================================
    // HELPERS DE MAPEO 
    // ==========================================
    
    private LedgerTransaction MapToDomain(LedgerTransactionDocument doc, string realId)
    {
        return new LedgerTransaction
        {
            Id = realId,
            Type = doc.Type ?? string.Empty,
            // Convertimos de double (Firestore) a decimal (C#)
            Amount = Convert.ToDecimal(doc.Amount),
            Concept = doc.Concept ?? string.Empty,
            Method = doc.Method ?? string.Empty,
            TransactionRef = doc.TransactionRef ?? string.Empty,
            RelatedUsers = new TransactionRelatedUsers
            {
                PayerName = doc.RelatedUsers?.PayerName ?? string.Empty,
                StudentName = doc.RelatedUsers?.StudentName ?? string.Empty
            },
            // Aseguramos que si no viene fecha, no truene
            CreatedAt = doc.CreatedAt == default ? DateTime.MinValue : doc.CreatedAt
        };
    }

    private LedgerTransactionDocument MapToDocument(LedgerTransaction transaction)
    {
        return new LedgerTransactionDocument
        {
            Type = transaction.Type,
            // Convertimos de decimal (C#) a double (Firestore)
            Amount = Convert.ToDouble(transaction.Amount),
            Concept = transaction.Type == "charge" ? transaction.Concept : string.Empty,
            Method = transaction.Type == "payment" ? transaction.Method : string.Empty,
            TransactionRef = transaction.Type == "payment" ? transaction.TransactionRef : string.Empty,
            RelatedUsers = new TransactionRelatedUsersDocument
            {
                PayerName = transaction.RelatedUsers.PayerName,
                StudentName = transaction.RelatedUsers.StudentName
            },
            // Aseguramos que la fecha vaya en formato UTC para Firestore
            CreatedAt = transaction.CreatedAt.Kind == DateTimeKind.Utc 
                        ? transaction.CreatedAt 
                        : transaction.CreatedAt.ToUniversalTime()
        };
    }
}