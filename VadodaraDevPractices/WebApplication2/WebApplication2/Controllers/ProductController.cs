using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopping.BLL;
using Shopping.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShoppingBusiness _shoppingBusiness;

        public ProductController(IShoppingBusiness shoppingBusiness)
        {
            _shoppingBusiness = shoppingBusiness;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewBag.IsVisible = false;
            var product = _shoppingBusiness.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Index(ShoppingProduct product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsVisible = false;
                return View(product);
            }

            if (product.PurchasedQuantity <= 0)
            {
                ModelState.AddModelError("PurchasedQuantity", "Please enter valid quantity");
                return View(product);
            }

            if (product.PurchasedQuantity > product.Quantity)
            {
                ModelState.AddModelError("PurchasedQuantity", "Sorry!!! There is not enough stock available");
                return View(product);
            }

            if (product.IsMerchantDiscountAllowed)
            {
                product = _shoppingBusiness.ApplyFestivalDiscount(product);
            }
            ViewBag.IsVisible = true;
            return View(product);
        }

        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }
    }
}
