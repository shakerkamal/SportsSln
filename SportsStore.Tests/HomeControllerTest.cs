using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Contracts;
using SportsStore.Controllers;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"}
            }.AsQueryable<Product>());

            HomeController homeController = new HomeController(mock.Object);

            //Act
            ProductListViewModel result = (homeController.Index(null) as ViewResult)
                .ViewData.Model as ProductListViewModel;

            //Assert
            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P1", products[0].Name);
            Assert.Equal("P2", products[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }.AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            controller.PageSize = 3;

            //Act
            ProductListViewModel result = (controller.Index(null,2) as ViewResult).ViewData.Model as ProductListViewModel;

            //Assert
            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        //[Fact]
        //public void Can_Generate_Page_Links()
        //{
        //    //Arrange
        //    var urlHelper = new Mock<IUrlHelper>();
        //    urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
        //        .Returns("Test/Page1")
        //        .Returns("Test/Page2")
        //        .Returns("Test/Page3");

        //    var urlHelperFactory = new Mock<IUrlHelperFactory>();
        //    urlHelperFactory.Setup(f =>
        //        f.GetUrlHelper(It.IsAny<ActionContext>()))
        //            .Returns(urlHelper.Object);

        //    PageLinkTagHelper helper =
        //        new PageLinkTagHelper(urlHelperFactory.Object)
        //        {
        //            PageModel = new PagingInfo
        //            {
        //                CurrentPage = 2,
        //                TotalItems = 28,
        //                ItemsPerPage = 10
        //            },
        //            PageAction = "Test"
        //        };
        //    TagHelperContext ctx = new TagHelperContext(
        //        new TagHelperAttributeList(),
        //        new Dictionary<object, object>(), "");
        //    var content = new Mock<TagHelperContent>();
        //    TagHelperOutput output = new TagHelperOutput("div",
        //        new TagHelperAttributeList(),
        //        (cache, encoder) => Task.FromResult(content.Object));

        //    //Act
        //    helper.Process(ctx, output);

        //    // Assert
        //    Assert.Equal(@"<a href=""Test/Page1"">1</a>"
        //            + @"<a href=""Test/Page2"">2</a>"
        //            + @"<a href=""Test/Page3"">3</a>",
        //            output.Content.GetContent());
        //}

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
                }).AsQueryable<Product>());
            // Arrange
            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };
            // Act
            ProductListViewModel result = (controller.Index(null, 2) as ViewResult)
                .ViewData.Model as ProductListViewModel;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 4;

            //Act
            Product[] products = ((controller.Index("Cat2",1) as ViewResult)
                .ViewData.Model as ProductListViewModel).Products.ToArray();

            //Assert
            Assert.Equal(2, products.Length);
            Assert.True(products[0].Name == "P2" && products[0].Category == "Cat2");
            Assert.True(products[1].Name == "P4" && products[0].Category == "Cat2");
        }
        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
                }).AsQueryable<Product>());
            HomeController target = new HomeController(mock.Object);
            target.PageSize = 3;
            Func<ViewResult, ProductListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductListViewModel;

            // Action
            int? res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.Index(null))?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
