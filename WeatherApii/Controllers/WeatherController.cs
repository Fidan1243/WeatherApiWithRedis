using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApii.Entities;
using WeatherApii.Statics;

namespace WeatherApii.Controllers
{
    [Route("Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IDistributedCache _redisCache;
        public WeatherController(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("id")]
        public async Task<Weather> GetWeather(int id)
        {
            var weather = await _redisCache.GetStringAsync($"{id} weather");
            if (weather != null)
            {
                return JsonConvert.DeserializeObject<Weather>(weather);

            }
            var js = WeatherList.Weathers.FirstOrDefault(w => w.Id == id);
            await _redisCache.SetStringAsync($"{id} weather", JsonConvert.SerializeObject(js));
            return js;
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteWeatherAsync(int id)
        {
            WeatherList.Weathers.Remove(WeatherList.Weathers.FirstOrDefault(w => w.Id == id));
            var weather = await _redisCache.GetStringAsync($"{id} weather");
            if (weather != null)
            {
                await _redisCache.RemoveAsync($"{id} weather");
            }
            return Ok();
        }

        [HttpPatch("id")]
        public async Task<IActionResult> UpdateWeatherAsync(int id, Weather weather)
        {
            var obj = WeatherList.Weathers.FirstOrDefault(x => x.Id == id);
            if (obj != null) obj = weather;
            var weatther = await _redisCache.GetStringAsync($"{id} weather");
            if(weather != null)
            {
                await _redisCache.SetStringAsync($"{id} weather", JsonConvert.SerializeObject(weather));
            }
            return Ok();

        }
    }
}
