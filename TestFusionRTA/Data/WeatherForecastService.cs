using Stl.Async;
using Stl.Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFusionRTA.Data
{

    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private List<WeatherForecast> _weathers;

  
        public WeatherForecastService()
        {
            var rng = new Random();
            _weathers = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList();
        }

        [ComputeMethod]
        public virtual Task<List<WeatherForecast>> GetForecastAsync()
        {
            return Task.FromResult(_weathers);
        }


        public Task Add()
        {
            var rng = new Random();
            _weathers.Add(new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            using (Computed.Invalidate())
            {
                this.GetForecastAsync().Ignore();
            }
            return Task.CompletedTask;
        }
    }
}
