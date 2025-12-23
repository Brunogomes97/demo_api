using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.API.Applications.Products.DTOs;
using project.API.Applications.Products.Services;
using System.Security.Claims;

namespace project.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service) => _service = service;

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var userId = GetUserId();
            var product = await _service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser()
        {
            var userId = GetUserId();
            var products = await _service.GetByUserAsync(userId);
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            var userId = GetUserId();
            var product = await _service.UpdateAsync(userId, id, dto);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            await _service.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}
