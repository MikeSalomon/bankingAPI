using AutoMapper;
using BankingSystem.API.Controllers;
using BankingSystem.API.Dtos;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace BankingSystem.UnitTests
{
    public class UsersControllerTests
    {
        private readonly Mock<IBankRepository> repositoryStub = new();
        private readonly Mock<IMapper> mapperStub = new();
        private readonly Random rand = new();

        [Fact]
        public void GetUserById_WithUnexistingUser_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).Returns((User)null);
            var controller = new UsersController(repositoryStub.Object, mapperStub.Object);

            // Act 
            var result = controller.GetUserById(Guid.NewGuid());

            // Assert 
            Assert.IsType<NotFoundResult>(result.Result); 
        }

        [Fact]
        public void GetUserById_WithExisitingUser_ReturnsExpectedUser()
        {
            // Arrange
            var expectedItem = CreateRandomUser(); 
            repositoryStub.Setup(repo => repo.GetUserById(It.IsAny<Guid>()))
                .Returns(expectedItem);

            var controller = new UsersController(repositoryStub.Object, mapperStub.Object);

            // Act
            var result = controller.GetUserById(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedItem,
                options => options.ComparingByMembers<User>()); 
        }

        private User CreateRandomUser()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
