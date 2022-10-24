using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Shared
{
    public partial class VestingInfo : ComponentBase
    {
        [Parameter]
        public SaleInfo? Sale { get; set; }
        VestingInfoData[]? Data
        {
            get
            {
                if (Sale != null)
                {
                    return BuildVestingInfo(Sale);
                }
                return null;
            }
        }

        private VestingInfoData[] BuildVestingInfo(SaleInfo sale)
        {
            var total = sale.VestingPortions.Length;
            var result = new List<VestingInfoData>();
            for (var i = 0; i < total; i++)
            {
                var portion = (float)sale.VestingPortions[i] * 100f / (float)sale.VestingPortionPrecision;
                var date = sale.VestingTimes[i].ToDateTimeOffset().UtcDateTime;
                result.Add(new VestingInfoData(portion, date,sale.VestingTimes[i]));
            }
            return result.ToArray();
        }

        public string RemainingTime(DateTime UtcDate)
        {
            var remaining = UtcDate - DateTime.UtcNow;
            if(remaining.TotalSeconds < 0){
                return "done";
            }
            List<string> parts = new List<string>();
            parts.Add(GetRemainingTimePart(remaining.Days, "day"));
            parts.Add(GetRemainingTimePart(remaining.Hours, "hour"));
            parts.Add(GetRemainingTimePart(remaining.Minutes, "minute"));
            parts.Add(GetRemainingTimePart(remaining.Seconds, "second"));
            return string.Join(" ", parts);
        }

        public string GetRemainingTimePart(int number, string label)
        {
            if (number == 0)
            {
                return string.Empty;
            }
            var result = $"{number} {label}";
            if (number > 1)
            {
                result += "s";
            }
            return result;
        }

    }
}
