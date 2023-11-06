using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class WeatherViewModel
    {
        public WeatherViewModel(Weather _weather, DailyForecast _dailyForecast, HourlyForecast _hourlyForecast,
        Weather _weatherHistorical, Indice _indice)
        {
            CurrentTemperature = _weather.Temperature.Metric.Value;
            CurrentWeatherText = _weather.WeatherText;

            TemperatureIn5DaysMax = _dailyForecast.Temperature.Maximum.Value;
            TemperatureIn5DaysMin = _dailyForecast.Temperature.Minimum.Value;
            WeatherIn5DaysText = _dailyForecast.Day.IconPhrase;

            TemperatureIn12Hours = _hourlyForecast.Temperature.Value;
            WeatherIn12HoursText = _hourlyForecast.IconPhrase;

            TemperatureLast6h = _weatherHistorical.Temperature.Metric.Value;
            WeatherTextLast6h = _weatherHistorical.WeatherText;

            IndiceName = _indice.Name;
            IndiceCategory = _indice.Category;
        }
        public double CurrentTemperature { get; set; }
        public double TemperatureIn5DaysMax { get; set; }
        public double TemperatureIn5DaysMin { get; set; }
        public double TemperatureIn12Hours { get; set; }
        public double TemperatureLast6h { get; set; }
        public string CurrentWeatherText { get; set; }
        public string WeatherIn5DaysText { get; set; }
        public string WeatherIn12HoursText { get; set; }
        public string WeatherTextLast6h { get; set; }
        public string IndiceName { get; set; }
        public string IndiceCategory { get; set; }
    }
}
