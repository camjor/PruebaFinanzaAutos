using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Asisya.Controllers;
using Asisya.Data.Products;
using Asisya.Models;
using Asisya.Dtos.ProductDtos;

public class ProductControllerTests
{
    [Fact]
    public async Task GetProducts_ReturnsOk()
    {
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Product> { new Product { ProductName = "Chai" } });

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<IEnumerable<ProductResponseDto>>(It.IsAny<IEnumerable<Product>>()))
                  .Returns(new List<ProductResponseDto> {
                      new ProductResponseDto { ProductName = "Chai" }
                  });

        var controller = new ProductController(mockRepo.Object, mockMapper.Object);

        var result = await controller.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
    }
}
