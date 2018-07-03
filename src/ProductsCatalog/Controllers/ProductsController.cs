using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCatalog.Contexts;
using ProductsCatalog.Models;

namespace ProductsCatalog.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;


        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET api/values
        [HttpGet]

        public async Task<ActionResult> Get() => Ok(await context.Products.ToListAsync());

        // GET api/values/5
        [HttpGet("{id:int}", Name ="GoToProduct")]
        public async Task<ActionResult> Get(int id)
        {
            Product p = await context.Products.Where(pt => pt.ProductId == id).SingleOrDefaultAsync();
            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpGet("{q}")]
        public async Task<IActionResult> Find([FromQuery]string q)
        {
            return Ok(await this.context
                                  .Products
                                  .Where(p => p.BriefDescription.Contains(q) || p.Name.Contains(q))
                                  .Take(10)
                                  .ToListAsync());
        }

        [HttpGet]
        [Route("namestartswith")]
        public async Task<IActionResult> ForAutoComplete([FromQuery]string name)
        {
            if (name?.Length > 0)
            {
                return Ok(await this.context
                                      .Products
                                      .Where(p => p.BriefDescription.Contains(name) || p.Name.Contains(name))
                                      //.Where(p => p.Name.ToLower().StartsWith(name.ToLower()))
                                      .Take(10)
                                      .ToListAsync());
            }
            return Ok(new List<Product>());
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] int id, [FromQuery] string name, [FromQuery] string description, [FromQuery] decimal price, [FromQuery]int quantity)
        {
            IQueryable<Product> source = this.context.Products;
            if (id > 0)
            {
                source = source.Where(p => p.ProductId == id);
            }
            if (name?.Length > 0)
            {
                source = source.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (description?.Length > 0)
            {
                source = source.Where(p => p.BriefDescription.ToLower().Contains(description.ToLower()));
            }
            if (price > 0)
            {
                source = source.Where(p => p.Price <= price);
            }
            if (quantity > 0)
            {
                source = source.Where(p => p.Quantity >= quantity);
            }
            return Ok(await source.ToListAsync());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                this.context.Products.Add(product);
                await this.context.SaveChangesAsync();
                return Created("GoToProduct", product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await this.context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

            if(item == null)
            {
                return NotFound();
            }

            item.Name = product.Name;
            item.BriefDescription = product.BriefDescription;
            item.Price = product.Price;
            item.Cost = product.Cost;
            item.Quantity = product.Quantity;
            
            await this.context.SaveChangesAsync();
            
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await this.context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            this.context.Products.Remove(item);
            await this.context.SaveChangesAsync();

            return Ok();

        }
    }
}
