using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrevisaoTempoApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "ca31da12def704e5c9c1282351d46c56";
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                return weatherData;
            }

            throw new Exception("Falha ao obter os dados da previsão do tempo.");
        }
    }

    public class WeatherData
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
    }

    public class MainData
    {
        public float Temp { get; set; }
        public float Humidity { get; set; }
    }
}