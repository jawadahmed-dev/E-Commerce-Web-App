using Core.Models.Product;
using Core.Models.Validators.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.Validators
{
    public class GetProductsModelValidatorTests
    {
        private readonly GetProductsModelValidator _validator;

        public GetProductsModelValidatorTests()
        {
            _validator = new GetProductsModelValidator();
        }


        [Fact]
        public void Should_PassValidation_When_ModelIsValid()
        {
            // Arrange
            var model = new GetProductsModel
            {
                PageIndex = 1,
                PageSize = 10,
                Sort = "Name",
                BrandId = 1,
                TypeId = 2,
                Search = "Product"
            };

            // Act
            var result = _validator.Validate(model);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_HaveValidationError_When_PageIndexIsInvalid(int pageIndex)
        {
            // Arrange
            var model = new GetProductsModel { PageIndex = pageIndex };

            // Act
            var result = _validator.Validate(model);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "PageIndex");
        }
    }
}
