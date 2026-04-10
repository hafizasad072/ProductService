using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.BO;
using Products.Models.AppContext;
using Products.Models.Entities;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = dto.Name,
                Colour = dto.Colour,
                Price = dto.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? colour)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(colour))
                query = query.Where(p => p.Colour.ToLower() == colour.ToLower());

            return Ok(await query.ToListAsync());
        }
    }
}
