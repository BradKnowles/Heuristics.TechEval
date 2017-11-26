using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tests.Integration
{
    [TestFixture]
    [Author("Brad Knowles")]
    [Category("Long Running")]
    [Category("Integration")]
    public class CategoryControllerTests
    {
        [Test]
        public void List_Having0Categories_ShouldReturn0()
        {
            // Arrange
            var sut = new CategoriesController();

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Category>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == 0);
        }

        [Test]
        public void List_Having2Categories_ShouldReturn2()
        {
            // Arrange
            var sut = new CategoriesController();
            var dbContext = new TestDataContext();
            dbContext.Categories.Add(new Category { Id = 1, Name = "Category 1" });
            dbContext.Categories.Add(new Category { Id = 2, Name = "Category 2" });
            dbContext.SaveChanges();

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Category>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == 2);
        }
    }
}
