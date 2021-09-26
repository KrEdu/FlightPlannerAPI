namespace FlightPlannerAPI.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }
        public PageResult(int page, int totalTimes, List<Flight> items)
        {
            Page = page;
            TotalItems = totalTimes;
            Items = items;
        }
    }
}
