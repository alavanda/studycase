using Catalog.Application.Features.Categories.Commands;
using Catalog.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCategoryQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCategory(string id)
        {
            var result = await _mediator.Send(new GetCategoryQuery { Id = id });

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
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.Data != null ? CreatedAtAction(nameof(GetCategory), new { id = result.Data }, command) : NotFound(null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCategory([FromRoute] string id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = string.IsNullOrEmpty(command.Id) ? id : command.Id;

            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest();
            }

            return result.IsSuccess ? NoContent() : NotFound(null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategory([FromRoute] string id)
        {
            var command = new DeleteCategoryCommand { Id = id };

            var result = await _mediator.Send(command);

            if (!result.IsValidResponse)
            {
                return BadRequest(result.ValidationErrors);
            }

            return result.IsSuccess ? NoContent() : NotFound(null);
        }
    }
}
