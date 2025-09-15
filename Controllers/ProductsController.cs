using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try { 
                _context.Products.Add(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created,product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding product to the database: " + ex.Message);
            }
            //return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, product);
        }
        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            var product = _context.Products.Find(id);
           
            return product;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No such product");
                }
                _context.Products.Update(product);
                _context.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product in the database: " + ex.Message);
            }
        }
    }
}
