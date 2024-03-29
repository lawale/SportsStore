﻿using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
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
            var result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            //Assert
            var products = result.Products.ToArray();
            Assert.True(products.Length == 3);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
            Assert.Equal("P6", products[2].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"},
                new Product { Id = 3, Name = "P3"},
                new Product { Id = 4, Name = "P4"},
                new Product { Id = 5, Name = "P5"}
            }).AsQueryable());

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            //Act
            var result = controller.List(null,2).ViewData.Model as ProductsListViewModel;

            //Assert
            var pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);

        }


    }
}
