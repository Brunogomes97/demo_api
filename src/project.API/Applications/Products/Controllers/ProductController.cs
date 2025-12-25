using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.API.Applications.Auth.Interfaces;
using project.API.Applications.Products.DTOs;
using project.API.Applications.Products.Services;

namespace project.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly ICurrentUserService _currentUser;

        public ProductsController(ProductService service, ICurrentUserService currentUser) => (_service, _currentUser) = (service, currentUser);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var userId = _currentUser.UserId;
            var product = await _service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser()
        {
            var userId = _currentUser.UserId;
            var products = await _service.GetByUserAsync(userId);
            return Ok(products);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
        [FromQuery] ProductPaginationQueryDto query)
        {
            var userId = _currentUser.UserId;
            var result = await _service.GetByUserPagedAsync(
                userId,
                query
            );
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            var userId = _currentUser.UserId;
            var product = await _service.UpdateAsync(userId, id, dto);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = _currentUser.UserId;
            await _service.DeleteAsync(userId, id);
            return Ok(new { success = true });
        }
    }
}
