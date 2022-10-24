using System.Reflection;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
namespace AvalaunchDashboard.Shared;

[FirestoreData]
public class UserVestingInfo
{
    [FirestoreProperty]
    public bool[] PortionWithdrawn { get; set; }
    [FirestoreProperty]
    public string TotalTokensStr
    {
        get
        {
            return TotalTokens.ToString();
        }
        set
        {
            TotalTokens = BigInteger.Parse(value);
        }
    }
    public BigInteger TotalTokens { get; set; }
    [FirestoreProperty]
    public string TotalAvaxStr
    {
        get
        {
            return TotalAvax.ToString();
        }
        set
        {
            TotalAvax = BigInteger.Parse(value);
        }
    }
    public BigInteger TotalAvax { get; set; }
    public UserVestingInfo() : this(new bool[0], 0, 0) { }
    public UserVestingInfo(bool[] portionWithdrawn, BigInteger totalTokens, BigInteger totalAvax)
    {
        this.TotalTokens = totalTokens;
        this.TotalAvax = totalAvax;
        this.PortionWithdrawn = portionWithdrawn;
    }
}