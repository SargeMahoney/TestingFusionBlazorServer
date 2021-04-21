using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestFusionRTA.Data
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetForecastAsync();
        Task Add();
    }
}