namespace WeatherApii.Entities
{
    public class Weather
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime When { get; set; }
        public string Description { get; set; }
    }
}
