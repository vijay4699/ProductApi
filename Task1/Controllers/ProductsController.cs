using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1.Model;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1,Name="Laptop",Price=1500,Stock=10},
            new Product { Id = 2,Name="Smartphone",Price=800,Stock=20},
        };

        [HttpGet]
        public IActionResult GetProducts() 
        {
            return Ok( _products );
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product=_products.FirstOrDefault(p=>p.Id== id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult AddProduct(Product newproduct) 
        {
            if(newproduct == null || newproduct.Price<=0 || newproduct.Stock<0)
                return BadRequest("Invalid Product data");
            newproduct.Id=_products.Max(p=>p.Id)+1;
            _products.Add(newproduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newproduct.Id }, newproduct);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,Product updateProduct)
        {
            var product=_products.FirstOrDefault(p=> p.Id== id);
            if(product == null)
            {
                return NotFound();
            }
            product.Name=updateProduct.Name;
            product.Price=updateProduct.Price;
            product.Stock=updateProduct.Stock;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product=_products.FirstOrDefault(p=>p.Id==id);
            if(product == null)
                return NotFound();
            _products.Remove(product);
            return NoContent();
        }
    }
}
