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
    public string AmountBoughtStr
    {
        get
        {
            return AmountBought.ToString();
        }
        set
        {
            AmountBought = BigInteger.Parse(value);
        }
    }
    public BigInteger AmountBought { get; set; }
    public UserVestingInfo() : this(new bool[0], 0) { }
    public UserVestingInfo(bool[] portionWithdrawn, BigInteger amountBought)
    {
        this.AmountBought = amountBought;
        this.PortionWithdrawn = portionWithdrawn;
    }
}