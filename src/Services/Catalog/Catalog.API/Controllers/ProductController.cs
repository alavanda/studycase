using Catalog.Application.Features.Products.Commands;
using Catalog.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetProduct([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetProductQuery { Id = id });

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.Data != null ? Ok(result.Data) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.Data != null ? CreatedAtAction(nameof(GetProduct), new { id = result.Data }, command) : NotFound(null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductCommand command)
        {
            command.Id = string.IsNullOrEmpty(command.Id) ? id : command.Id;

            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.IsSuccess ? NoContent() : NotFound(null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProduct([FromRoute] string id)
        {
            var command = new DeleteProductCommand { Id = id };

            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.IsSuccess ? NoContent() : NotFound(null);
        }
    }
}
