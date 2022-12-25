using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab5.WebApp.ApiWrapper;
using DishShop.Lab5.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DishShop.Lab5.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApiWrapper.ApiWrapper _apiWrapper = new ApiWrapper.ApiWrapper();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Dish> dishes = new List<Dish>();

            try
            {
                dishes = await _apiWrapper.GetDishes();
            }
            catch(Exception ex)
            {
                return BadRequest("Connection error");
            }

            if (dishes == null || !dishes.Any())
            {
                return BadRequest("No data");
            }

            return View(dishes);
        }

        public IActionResult SetDish()
        {
            return View();  
        }

        public async Task<IActionResult> CreateDish(Dish dish)
        {
            await _apiWrapper.SetDish(dish);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}