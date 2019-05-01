using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product {Id = 1, Name = "P1" },
                    new Product {Id = 2, Name = "P2" },
                    new Product {Id = 3, Name = "P3" },
                    new Product {Id = 4, Name = "P4" },
                    new Product {Id = 5, Name = "P5" },
                    new Product {Id = 6, Name = "P6" }
                }).AsQueryable());

            var controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //Act
            var result = controller.List(2).ViewData.Model as IEnumerable<Product>;

            //Assert
            var products = result.ToArray();
            Assert.True(products.Length == 3);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
            Assert.Equal("P6", products[2].Name);
        }
    }
}
