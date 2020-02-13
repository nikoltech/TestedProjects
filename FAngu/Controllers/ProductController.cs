namespace FAngu.Controllers
{
    using FAngu.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly AppContext _context;

        public ProductController(AppContext context)
        {
            this._context = context;
            if (!this._context.Products.Any())
            {
                this._context.Products.Add(new Product { Name = "iPhone X", Company = "Apple", Price = 79900 });
                this._context.Products.Add(new Product { Name = "Galaxy S8", Company = "Samsung", Price = 49900 });
                this._context.Products.Add(new Product { Name = "Pixel 2", Company = "Google", Price = 52900 });
                this._context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this._context.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = this._context.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                this._context.Products.Add(product);
                this._context.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                this._context.Update(product);
                this._context.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = this._context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                this._context.Products.Remove(product);
                this._context.SaveChanges();
            }
            return Ok(product);
        }
    }
}
