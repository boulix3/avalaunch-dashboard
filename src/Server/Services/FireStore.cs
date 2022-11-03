
using AvalaunchDashboard.Shared;
using AvalaunchDashboard.Web3;
using Google.Cloud.Firestore;

namespace AvalaunchDashboard.Server.Services;
public class FireStore
{
    private FirestoreDb _db;
    private ContractsService _web3;

    private FireStore(FirestoreDb db, ContractsService web3)
    {
        this._db = db;
        this._web3 = web3;
    }

    public static FireStore Init(ContractsService web3)
    {
        var fire = Environment.GetEnvironmentVariable("FIRE");
        System.Diagnostics.Trace.TraceError("FireStore init - Fire environment variable : " + fire);
        var builder = new Google.Cloud.Firestore.V1.FirestoreClientBuilder();
        builder.JsonCredentials = fire;
        var client = builder.Build();
        var db = FirestoreDb.Create("avalaunch-dashboard", client);
        return new FireStore(db, web3);
    }
    internal async Task<Dictionary<string, UserData>> UserHistory()
    {
        var result = new Dictionary<string, UserData>();
        var c = _db.Collection("userData");
        var docs = c.ListDocumentsAsync();
        await foreach (var item in docs)
        {
            var snapshot = await item.GetSnapshotAsync();
            var userData = snapshot.ConvertTo<UserData>();
            result.Add(item.Id, userData);
        }
        return result;
    }
    internal async Task<UserData> GetUserData(string address)
    {
        var d = _db.Document($"userData/{address}");
        var snapshot = await d.GetSnapshotAsync();
        var result = snapshot.ConvertTo<UserData>();
        return result ?? new UserData();
    }
    public async Task<UserData> ImportUserData(string address)
    {
        var saleContracts = await GetSales();
        var userInfo = await _web3.GetUserData(address, saleContracts);
        var d = _db.Document($"userData/{address}");
        await d.SetAsync(userInfo);
        return userInfo;
    }
    public async Task<SaleData> ImportSalesData()
    {
        var saleInfos = await _web3.GetSalesData(ContractsService.factories);
        var d = _db.Document("sales/info");
        await d.SetAsync(saleInfos);
        return saleInfos;
    }

    public async Task DeleteSalesData()
    {
        var c = _db.Collection("sales");
        await DeleteCollection(c, 100);
    }

    internal async Task<SaleData> GetSales()
    {
        var d = _db.Document("sales/info");
        var snapshot = await d.GetSnapshotAsync();
        var result = snapshot.ConvertTo<SaleData>();
        return result ?? new SaleData();
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
