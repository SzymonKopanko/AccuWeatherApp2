using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel _selectedCity;
        private DailyForecast _dailyForecast;
        private HourlyForecast _hourlyForecast;
        private Weather _weather;
        private Weather _weatherHistorical;
        private Indice _indice;
        private readonly IAccuWeatherService _accuWeatherService;

        public MainViewModel(IAccuWeatherService accuWeatherService)
        {
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>(); // podejście nr 2 
        }

        [ObservableProperty]
        private WeatherViewModel weatherView;


        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }


        private async void LoadWeather()
        {
            if (SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key);
                _dailyForecast = await _accuWeatherService.GetDailyForecast(SelectedCity.Key);
                _hourlyForecast = await _accuWeatherService.GetHourlyForecast(SelectedCity.Key);
                _weatherHistorical = await _accuWeatherService.GetHistoricalConditions(SelectedCity.Key);
                _indice = await _accuWeatherService.GetIndices(SelectedCity.Key);
                WeatherView = new WeatherViewModel(_weather, _dailyForecast, _hourlyForecast, _weatherHistorical, _indice);
            }
        }

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(new CityViewModel(city));
        }
    }
}
