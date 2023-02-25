using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApii.DataAccess;
using WeatherApii.Entities;
using WeatherApii.Statics;

namespace WeatherApii.Controllers
{
    [Route("Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IDistributedCache _redisCache;
        private DataContext _context;

        public WeatherController(IDistributedCache cache, DataContext context)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
            _context = context;
        }

        [HttpGet("id")]
        public async Task<Weather> GetWeather(int id)
        {
            var weather = await _redisCache.GetStringAsync($"{id} weather");
            if (weather != null)
            {
                return JsonConvert.DeserializeObject<Weather>(weather);

            }
            var js = _context.Weather.FirstOrDefault(w => w.Id == id);
            await _redisCache.SetStringAsync($"{id} weather", JsonConvert.SerializeObject(js));
            return js;
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteWeatherAsync(int id)
        {
            _context.Weather.Remove(await GetWeather(id));
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
            var obj = await GetWeather(id);
            if (obj != null) { 
                _context.Weather.Update(weather);
            }
            var weatther = await _redisCache.GetStringAsync($"{id} weather");
            if(weather != null)
            {
                await _redisCache.SetStringAsync($"{id} weather", JsonConvert.SerializeObject(weather));
            }
            return Ok();

        }
    }
}
