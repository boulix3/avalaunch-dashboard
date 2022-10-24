using System.Numerics;
using System.IO.IsolatedStorage;
using Google.Cloud.Firestore;
namespace AvalaunchDashboard.Shared;


[FirestoreData]
public class SaleInfo
{
    public SaleInfo() : this(string.Empty, string.Empty, string.Empty, 0, string.Empty,
        0, false, new long[0], new long[0], 0)
    { }
    public SaleInfo(string address, string tokenName, string tokenSymbol, int tokenDecimals, string tokenAddress,
        long time, bool isOldContract, long[] vestingTimes, long[] vestingPortions, long vestingPortionPrecision)
    {
        Address = address;
        TokenName = tokenName;
        TokenSymbol = tokenSymbol;
        TokenDecimals = tokenDecimals;
        TokenAddress = tokenAddress;
        Time = time;
        VestingTimes = vestingTimes;
        VestingPortions = vestingPortions;
        VestingPortionPrecision = vestingPortionPrecision;
        IsOldContract = isOldContract;
    }
    [FirestoreProperty]
    public string Address { get; set; }
    [FirestoreProperty]
    public string TokenName { get; set; }
    [FirestoreProperty]
    public string TokenSymbol { get; set; }
    [FirestoreProperty]
        public string TokenAddress { get; set; }
    [FirestoreProperty]
    public int TokenDecimals { get; set; }
    [FirestoreProperty]
    public long Time { get; set; }
    [FirestoreProperty]
    public long[] VestingTimes { get; set; }
    [FirestoreProperty]
    public long[] VestingPortions { get; set; }
    [FirestoreProperty]
    public long VestingPortionPrecision { get; set; }
    [FirestoreProperty]
    public bool IsOldContract { get; set; }

}