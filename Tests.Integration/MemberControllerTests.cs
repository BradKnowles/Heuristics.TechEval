using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Controllers;
using Heuristics.TechEval.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Tests.Integration
{
    [TestFixture]
    [Author("Brad Knowles")]
    [Category("Long Running")]
    [Category("Integration")]
    public class MemberControllerTests
    {
        [Test]
        public void NewMember_WhenAdding_ShouldSucceed()
        {
            // Arrange
            var sut = new MembersController();
            var newMember = new NewMember
            {
                Name = "John Smith",
                Email = "thedoctor@gallifrey.net"
            };

            // Act
            var result = sut.New(newMember);

            // Assert
            var context = new TestDataContext();
            var lastAddedmember = context.Members.OrderByDescending(x => x.Id).First();
            Assert.AreEqual(newMember.Name, lastAddedmember.Name);
            Assert.AreEqual(newMember.Email, lastAddedmember.Email);
        }

        [Test]
        public void List_Having0Members_ShouldReturn0()
        {
            // Arrange
            var sut = new MembersController();

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Member>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == 0);
        }

        [Test]
        public void List_Having2Members_ShouldReturn2()
        {
            // Arrange
            var sut = new MembersController();
            sut.New(new NewMember { Name = "Person 1", Email = "person1@example.com" });
            sut.New(new NewMember { Name = "Person 2", Email = "person2@example.com" });

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Member>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == 2);
        }
    }
}
