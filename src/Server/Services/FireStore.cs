
using AvalaunchDashboard.Shared;
using Google.Cloud.Firestore;

namespace AvalaunchDashboard.Server.Services;
public class FireStore
{
    private FirestoreDb _db;
    private Web3 _web3;

    private FireStore(FirestoreDb db, Web3 web3)
    {
        this._db = db;
        this._web3 = web3;
    }

    public static FireStore Init(Web3 web3)
    {
        var fire = Environment.GetEnvironmentVariable("FIRE");
        System.Diagnostics.Trace.TraceError("FireStore init - Fire environment variable : " + fire);
        var builder = new Google.Cloud.Firestore.V1.FirestoreClientBuilder();
        builder.JsonCredentials = fire;
        var client = builder.Build();
        var db = FirestoreDb.Create("avalaunch-dashboard", client);
        return new FireStore(db, web3);
    }

    public async Task<IEnumerable<SaleInfo>> ImportData()
    {
        var saleInfos = await _web3.GetSalesInfos(Web3.factories);
        var c = _db.Collection("sales");
        foreach (var item in saleInfos)
        {
            await c.AddAsync(item);
        }
        return saleInfos;
    }

    public async Task DeleteData()
    {
        var c = _db.Collection("sales");
        await DeleteCollection(c, 100);
    }

    internal async Task<IEnumerable<SaleInfo>> GetSales()
    {
        var c = _db.Collection("sales");
        var snapshot = await c.GetSnapshotAsync();
        var data = snapshot.Documents;
        return data.Select(x => x.ConvertTo<SaleInfo>());
    }

    private static async Task DeleteCollection(CollectionReference collectionReference, int batchSize)
    {
        QuerySnapshot snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
        IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
        while (documents.Count > 0)
        {
            foreach (DocumentSnapshot document in documents)
            {
                Console.WriteLine("Deleting document {0}", document.Id);
                await document.Reference.DeleteAsync();
            }
            snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
            documents = snapshot.Documents;
        }
        Console.WriteLine("Finished deleting all documents from the collection.");
    }
}
