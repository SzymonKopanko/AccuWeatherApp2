using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);

        Task<DailyForecast> GetDailyForecast(string cityKey);
        Task<HourlyForecast> GetHourlyForecast(string cityKey);
        Task<Weather> GetHistoricalConditions(string cityKey);
        Task<AlarmData[]> GetAlarms(string cityKey);
        Task<Indice> GetIndices(string cityKey);
    }
}
