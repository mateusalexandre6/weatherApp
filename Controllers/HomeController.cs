using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrevisaoTempoApp.Models;
using PrevisaoTempoApp.Services;

namespace PrevisaoTempoApp.Controllers
{

    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(city);
                return View(weatherData);
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao obter os dados da previsão do tempo.");
                return View();
            }
        }
    }
}
