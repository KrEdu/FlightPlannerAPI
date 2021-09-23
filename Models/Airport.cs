namespace FlightPlannerAPI.Models
{
    public class Airport
    {
        //public int Id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string airport { get; set; }
        
        public bool Contains( string phrase)
        {
            if (country.ToLower().Contains(phrase.ToLower().Trim()) 
                || city.ToLower().Contains(phrase.ToLower().Trim()) 
                || airport.ToLower().Contains(phrase.ToLower().Trim()))
                return true;
            else
                return false;
        }
    }
}
