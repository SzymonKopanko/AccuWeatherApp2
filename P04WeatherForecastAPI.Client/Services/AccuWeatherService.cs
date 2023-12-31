﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    internal class AccuWeatherService : IAccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language{2}&metric=true";

        private const string daily_forecast_endpoint = "forecasts/v1/daily/5day/{0}?apikey={1}&language={2}&metric=true";
        private const string hourly_forecast_endpoint = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language={2}&metric=true";
        private const string historical_conditions_endpoint = "currentconditions/v1/{0}/historical?apikey={1}&language{2}";
        private const string alarms_endpoint = "alarms/v1/1day/{0}?apikey={1}&language{2}";
        private const string indices_endpoint = "indices/v1/daily/1day/{0}?apikey={1}&language{2}";

        // private const string api_key = "5hFl75dja3ZuKSLpXFxUzSc9vXdtnwG5";
        string api_key;
        //private const string language = "pl";
        string language;

        public AccuWeatherService()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json");

            var configuration = builder.Build();
            api_key = configuration["api_key"];
            language = configuration["default_language"];
        }


        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;

            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }


        public async Task<DailyForecast> GetDailyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(daily_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                return data.DailyForecasts.ToArray()[4];
            }
        }

        public async Task<HourlyForecast> GetHourlyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(hourly_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                HourlyForecast[] data = JsonConvert.DeserializeObject<HourlyForecast[]>(json).ToArray();
                return data[11];
            }
        }

        public async Task<Weather> GetHistoricalConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json).ToArray();
                return weathers[5];
            }
        }

        public async Task<AlarmData[]> GetAlarms(string cityKey)
        {
            string uri = base_url + "/" + string.Format(alarms_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                AlarmData[] alarms = JsonConvert.DeserializeObject<AlarmData[]>(json);
                return alarms;

            }
        }
        public async Task<Indice> GetIndices(string cityKey)
        {
            string uri = base_url + "/" + string.Format(indices_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                Indice[] indices = JsonConvert.DeserializeObject<Indice[]>(json);
                return indices.FirstOrDefault();
            }
        }
    }
}
