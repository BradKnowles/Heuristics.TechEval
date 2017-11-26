using Heuristics.TechEval.Web.Controllers;
using Heuristics.TechEval.Web.Models;
using NUnit.Framework;
using System.Linq;

namespace Tests.Integration
{
    [TestFixture]
    [Author("Brad Knowles")]
    public class MemberControllerTests
    {
        [Test]
        [Category("Long Running")]
        [Category("Integration")]
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
            Assert.AreEqual(true, context.Members.Count() == 1);
        }
    }
}
