using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlymppMarketsAPI.Application.Commands;
using OlymppMarketsAPI.Application.Queries;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.DTOs;
namespace OlymppMarketsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        #endregion
        #region POST
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }
        #endregion
        #region PUT
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDto)
        {
            var command = new UpdateProductCommand(productDto);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion
    }
}
