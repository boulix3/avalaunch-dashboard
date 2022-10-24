using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AvalaunchDashboard.Shared;
[FirestoreData]

public class UserData
{
    public UserData() : this(0, new Dictionary<string, UserVestingInfo>()) { }
    public UserData(long lastUpdated, Dictionary<string, UserVestingInfo> vestingInfo)
    {
        LastUpdated = lastUpdated;
        VestingInfo = vestingInfo;
    }
    [FirestoreProperty]
    public long LastUpdated { get; set; }
    [FirestoreProperty]
    public Dictionary<string, UserVestingInfo> VestingInfo { get; set; }
}
