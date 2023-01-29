using StackExchange.Redis;
using System.Net.NetworkInformation;
using WeatherApii.Entities;

namespace WeatherApii.Statics
{
    public static class WeatherList
    {
        public static List<Weather> Weathers { get; set; } = new List<Weather>()
            {
                new Weather()
        {
            Id = 1,
                    Status = "Sunny",
                    Country = "Australia",
                    City = "Sydney",
                    When = new DateTime(2023, 02, 12, 11, 22, 11),
                    Description = "Today is sunny and hot"

                },
                new Weather()
        {
            Id = 2,
                    Status = "Cloudy",
                    Country = "Azerbaijan",
                    City = "Baku",
                    When = new DateTime(2023, 01, 29, 11, 22, 11),
                    Description = "Today is cold, cloudy and humid"

                },
                new Weather()
        {
            Id = 3,
                    Status = "Partly cloudy",
                    Country = "Japan",
                    City = "Tokyo",
                    When = new DateTime(2023, 01, 25, 11, 22, 11),
                    Description = "Today is partly cloudy and cold"

                },
                new Weather()
        {
            Id = 4,
                    Status = "Snowy",
                    Country = "Norway",
                    City = "Oslo",
                    When = new DateTime(2023, 02, 1, 11, 22, 11),
                    Description = "Today is sunny and hot"

                }
        };

    }
}
