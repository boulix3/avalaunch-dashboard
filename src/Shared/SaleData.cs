using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AvalaunchDashboard.Shared
{
    [FirestoreData]
    public class SaleData : DataDocumentDictionary<SaleInfo>
    {
        public SaleData() : base()
        {
        }

        public SaleData(long lastUpdated, Dictionary<string, SaleInfo> items) : base(lastUpdated, items)
        {
        }
    }
}