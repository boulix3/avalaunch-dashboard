using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AvalaunchDashboard.Shared
{
    [FirestoreData]
    public class DataDocumentDictionary<T>
    {
        public DataDocumentDictionary() : this(0, new Dictionary<string, T>()) { }
        public DataDocumentDictionary(long lastUpdated, Dictionary<string, T> items)
        {
            LastUpdated = lastUpdated;
            Items = items;
        }
        [FirestoreProperty]
        public long LastUpdated { get; set; }
        [FirestoreProperty]
        public Dictionary<string, T> Items { get; set; }
    }
}