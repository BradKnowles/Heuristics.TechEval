﻿using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Web.Controllers;
using Heuristics.TechEval.Web.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        public void NewMember_WhenAddingABlankUserName_ShouldThrowException()
        {
            // Arrange
            var sut = new MembersController();
            var newMember = new NewMember
            {
                Email = "thedoctor@gallifrey.net"
            };

            // Act

            // Assert
            Assert.Throws<DbEntityValidationException>(() => sut.New(newMember));
        }

        [Test]
        public void NewMember_WhenAddingABlankEmail_ShouldThrowException()
        {
            // Arrange
            var sut = new MembersController();
            var newMember = new NewMember
            {
                Name = "John Smith"
            };

            // Act

            // Assert
            Assert.Throws<DbEntityValidationException>(() => sut.New(newMember));
        }

        [Test]
        public void NewMember_WhenAddingDuplicateEmail_ShouldThrowException()
        {

            // Arrange
            var sut = new MembersController();
            var newMember = new NewMember
            {
                Name = "Person 6",
                Email = "duplicate_email_test@example.net"
            };

            // Act
            sut.New(newMember);

            // Assert
            Assert.Throws<DbUpdateException>(() => sut.New(newMember));
        }

        [Test]
        public void List_WhenDisplayingList_ShouldReturnCurrentMembers()
        {
            // Arrange
            var sut = new MembersController();
            var db = new TestDataContext();
            var currentMemberCount = db.Members.Count();

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Member>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == currentMemberCount);
        }

        [Test]
        public void List_Adding2Members_ShouldReturnListWithNewMembersAdded()
        {
            // Arrange
            var db = new TestDataContext();
            var currentMemberCount = db.Members.Count();

            var sut = new MembersController();
            sut.New(new NewMember { Name = "Person 1", Email = "person1@example.com" });
            sut.New(new NewMember { Name = "Person 2", Email = "person2@example.com" });

            // Act
            var result = sut.List() as ViewResult;

            // Assert
            var models = (List<Member>)result.ViewData.Model;
            Assert.AreEqual(true, models.Count == currentMemberCount + 2);
        }

        [Test]
        public void EditMember_MakesValidEdit_ShouldSucceed()
        {
            // Arrange
            var sut = new MembersController();
            var actionResult = sut.New(new NewMember { Name = "Person 3", Email = "person3@example.com" });
            var jsonResult = (JsonResult)actionResult;
            var newMember = JsonConvert.DeserializeObject<Member>(jsonResult.Data.ToString());

            var editedMember = new EditMember { Id = newMember.Id, Name = newMember.Name, Email = newMember.Email };
            editedMember.Name = "EDITED Person 3";

            // Act
            sut.Edit(editedMember);

            // Assert
            var context = new TestDataContext();
            var databaseMember = context.Members.First(x => x.Id == newMember.Id);

            Assert.AreEqual(editedMember.Id, databaseMember.Id);
            Assert.AreEqual(editedMember.Name, databaseMember.Name);
            Assert.AreEqual(editedMember.Email, databaseMember.Email);
        }

        [Test]
        public void EditMember_TryingToEditANonExistingMember_ShouldReturnEmptyResult()
        {
            // Arrange
            var editedMember = new EditMember { Id = -999, Name = "No one", Email = "noone@example.com" };

            // Act
            var sut = new MembersController();
            var editResult = sut.Edit(editedMember);

            // Assert
            Assert.IsInstanceOf(typeof(EmptyResult), editResult);
        }

        [Test]
        public void EditMember_WhenEditingABlankUserName_ShouldThrowException()
        {
            // Arrange
            var sut = new MembersController();
            var actionResult = sut.New(new NewMember { Name = "Person 4", Email = "person4@example.com" });
            var jsonResult = (JsonResult)actionResult;
            var newMember = JsonConvert.DeserializeObject<Member>(jsonResult.Data.ToString());

            var editedMember = new EditMember { Id = newMember.Id, Name = newMember.Name, Email = newMember.Email };
            editedMember.Name = String.Empty;

            // Act & Assert
            Assert.Throws<DbEntityValidationException>(() => sut.Edit(editedMember));
        }

        [Test]
        public void EditMember_WhenEditingABlankEmail_ShouldThrowException()
        {
            // Arrange
            var sut = new MembersController();
            var actionResult = sut.New(new NewMember { Name = "Person 5", Email = "person5@example.com" });
            var jsonResult = (JsonResult)actionResult;
            var newMember = JsonConvert.DeserializeObject<Member>(jsonResult.Data.ToString());

            var editedMember = new EditMember { Id = newMember.Id, Name = newMember.Name, Email = newMember.Email };
            editedMember.Email = String.Empty;

            // Act & Assert
            Assert.Throws<DbEntityValidationException>(() => sut.Edit(editedMember));
        }

        [Test]
        public void EditMember_WhenEditingCausesDuplicateEmail_ShouldThrowException()
        {
            // Arrange
            var sut = new MembersController();
            var actionResult = sut.New(new NewMember { Name = "Person 7", Email = "person7@example.com" });
            var jsonResult = (JsonResult)actionResult;
            var PERSON_7 = JsonConvert.DeserializeObject<Member>(jsonResult.Data.ToString());
            sut.New(new NewMember { Name = "Person 8", Email = "person8@example.com" });

            // Act & Assert
            var edited_PERSON_7 = new EditMember { Id = PERSON_7.Id, Name = PERSON_7.Name, Email = "person8@example.com" };
            Assert.Throws<DbUpdateException>(() => sut.Edit(edited_PERSON_7));
        }
    }
}
