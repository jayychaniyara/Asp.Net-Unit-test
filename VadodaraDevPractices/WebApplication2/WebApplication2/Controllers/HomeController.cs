using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.BLL;
using Shopping.Lib;
using System.Collections.Generic;
using System.Diagnostics;
using ShoppingApplication.Models;

namespace ShoppingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoppingBusiness _shoppingBusiness;

        public HomeController(ILogger<HomeController> logger,
            IShoppingBusiness shoppingBusiness)
        {
            _logger = logger;
            _shoppingBusiness = shoppingBusiness;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = GetProducts();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
        }

        private IEnumerable<ShoppingProduct> GetProducts()
        {
            var products = _shoppingBusiness.GetProducts();
            return products;
        }
    }
}
