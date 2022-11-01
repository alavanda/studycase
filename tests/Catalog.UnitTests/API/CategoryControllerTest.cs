using Catalog.API.Controllers;
using Catalog.Application.Features;
using Catalog.Application.Features.Categories.Queries;
using Catalog.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests.API
{
    public class CategoryControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        public CategoryControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetCategory_WithCategoryId_Status200OK()
        {
            //Arrange
            var fakeCategory = CatalogContextSeed.CreateCategories().First();

            var getCategoryQueryResponse = new GetCategoryQueryResponse { Id = fakeCategory.Id, Name = fakeCategory.Name, Description = fakeCategory.Description };

            var fakeCategoryResponse = new ApiResponse<GetCategoryQueryResponse> { Data = getCategoryQueryResponse };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCategoryQuery>(), default))
                .Returns(Task.FromResult(fakeCategoryResponse));

            //Act
            var categoryController = new CategoryController(_mediatorMock.Object);
            var actionResult = await categoryController.GetCategory(fakeCategory.Id) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetCategory_WithCategoryId_Status204NoContent()
        {
            //Arrange
            ApiResponse<GetCategoryQueryResponse> fakeCategoryResponse = new() { Data = null };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCategoryQuery>(), default))
                .Returns(Task.FromResult(fakeCategoryResponse));

            //Act
            var categoryController = new CategoryController(_mediatorMock.Object);
            var actionResult = await categoryController.GetCategory(ObjectId.GenerateNewId().ToString()) as NoContentResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetCategory_WithCategoryId_Status400BadRequest()
        {
            //Arrange
            ApiResponse<GetCategoryQueryResponse> fakeCategoryResponse = new(new List<string> { "Validasyon hatası" }) { Data = null };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCategoryQuery>(), default))
                .Returns(Task.FromResult(fakeCategoryResponse));

            //Act
            var categoryController = new CategoryController(_mediatorMock.Object);
            var actionResult = await categoryController.GetCategory("1") as BadRequestObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, StatusCodes.Status400BadRequest);
        }
    }
}
