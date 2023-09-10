using Api.Controllers;
using Api.Exceptions;
using Api.Models;
using AutoMapper;
using Core.Entites;
using Core.Interfaces.Repositories;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.controllers
{
    public class ProductsControllerTest
    {
        public readonly ProductsController _sut;
        public readonly Mock<IGenericRepository<Product>> _productRepoMock;
        public readonly Mock<IGenericRepository<ProductBrand>> _productBrandRepoMock;
        public readonly Mock<IGenericRepository<ProductType>> _productTypeRepoMock;
        public readonly Mock<IMapper> _mapperMock;

        public ProductsControllerTest()
        {
            // Mock the dependencies
            _productRepoMock = new Mock<IGenericRepository<Product>>();
            _productBrandRepoMock = new Mock<IGenericRepository<ProductBrand>>();
            _productTypeRepoMock = new Mock<IGenericRepository<ProductType>>();
            _mapperMock = new Mock<IMapper>();

            _sut = new ProductsController(_mapperMock.Object, 
                _productBrandRepoMock.Object, 
                _productTypeRepoMock.Object, 
                _productRepoMock.Object);
        }


        [Fact]
        public async Task GetProductByIdAsync_ReturnsOkResult_WhenProductFound()
        {
            // Arrange

            int productId = 1;
            
            var product = new Product { Id = 1, Name = "John Doe" };
            var productResponseModel = new ProductResponseModel { Id = 1, Name = "John Doe" };


            // Mock the behavior of the repository
            _productRepoMock.Setup(repo => repo.GetEntityWithSpec(It.IsAny<GetProductsWithTypeAndBrandSpecification>()))
                .ReturnsAsync(product); // Provide the expected product entity

            // Mock the IMapper
            _mapperMock.Setup(mapper => mapper.Map<ProductResponseModel>(It.IsAny<Product>()))
                .Returns(productResponseModel); // Provide the expected mapping result

            // Act
            var result = await _sut.GetProductByIdAsync(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<ProductResponseModel>>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(productId, response.Result.Id);

        }

        [Fact]
        public async Task GetProductByIdAsync_ThrowsNotFoundException_WhenNoProductFound()
        {
            // Arrange

            int productId = 1;

            // Mock the behavior of the repository
            _productRepoMock.Setup(repo => repo.GetEntityWithSpec(It.IsAny<GetProductsWithTypeAndBrandSpecification>()))
                .ReturnsAsync((Product) null); // Provide the expected product entity

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.GetProductByIdAsync(productId));

        }

    }
}
