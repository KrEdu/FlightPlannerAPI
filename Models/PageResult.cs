using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerAPI.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Flight[] Items { get; set; }
        public PageResult(int page, int totalTimes, Flight[] items)
        {
            Page = page;
            TotalItems = totalTimes;
            Items = items;
        }
    }
}
