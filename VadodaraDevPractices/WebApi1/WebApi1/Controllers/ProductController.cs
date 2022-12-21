using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingApi.Models;
using System.Collections.Generic;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public IProductRepository Products { get; set; }

        public ProductController(IProductRepository products)
        {
            Products = products;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Product> GetAll()
        {
            return Products.GetAll();
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var item = Products.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Products.Add(item);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var product = Products.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            Products.Update(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public Product Delete(int id)
        {
            return Products.Remove(id);
        }

        [HttpGet("GetProductsPaging")]
        public IActionResult GetProductsWithPaging([FromQuery] QueryParameters queryParameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            PagedList<Product> products = Products.GetPagedProducts(queryParameters);

            var paginationMetadata = new
            {
                totalCount = products.TotalCount,
                pageSize = products.PageSize,
                currentPage = products.CurrentPage,
                totalPages = products.TotalPages,
                previousPage = products.HasPrevious,
                nextPage = products.HasNext
            };

            //Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(products);
        }
    }
}
