using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupM.Content.Persistence.Test
{
    [TestFixture]
    public class StaticUserTextsRepositoryTest
    {
        private IUnityContainer unityContainer;
        private IUserTextsRepository repository;

        [OneTimeSetUp]
        public void Init()
        {

        }

        [SetUp]
        public void SetupTest()
        {
            repository = new StaticUserTextsRepository();
        }

        [Test]
        public void StaticUserTextsRepository_ShouldHavePredefinedData()
        {
            // Arrange

            // Act
            var predefinedItems = repository.GetAll();
            var numberOfItems = predefinedItems.Count();

            // Assert
            Assert.That(numberOfItems, Is.EqualTo(1));
        }

        [Test]
        public void StaticUserTextsRepository_ShouldReturnById()
        {
            // Arrange
            var expectedItem = new UserText() { Id = 1, Text = "The weather in London in August is bad. Is like winter, horrible" };

            // Act
            var item = repository.Get(1);

            // Assert
            Assert.IsTrue(expectedItem.Id == item.Id && expectedItem.Text == item.Text);
        }

        [Test]
        public void StaticUserTextsRepository_ShouldReturnNullIfDoesntExist()
        {
            // Arrange

            // Act & Assert
            Assert.IsNull(repository.Get(10));
        }

        [Test]
        public void StaticUserTextsRepository_ShouldAddAndReturnItem()
        {
            // Arrange
            var newItem = new UserText() { Text = "This is a test text" };

            // Act
            repository.Add(newItem);
            var addedItem = repository.Get(newItem.Id);

            // Assert
            Assert.AreEqual(newItem, addedItem);
        }

        [Test]
        public void StaticUserTextsRepository_ShouldUpdateItem()
        {
            // Arrange
            var replacement = new UserText() { Id = 1, Text = "This is a test text replacement" };

            // Act
            repository.Update(replacement);
            var updatedItem = repository.Get(1);

            // Assert
            Assert.IsTrue(updatedItem.Id == replacement.Id && updatedItem.Text == replacement.Text);
        }

        [Test]
        public void StaticUserTextsRepository_ShouldReturnNullOnDeletedItem()
        {
            // Arrange

            // Act
            repository.Delete(0);

            // Assert
            Assert.IsNull(repository.Get(0));
        }

        [Test]
        public void StaticUserTextsRepository_ShouldNotBeThrowingNotFound()
        {
            // Arrange

            // Act
            repository.Delete(1);

            // Assert
            Assert.DoesNotThrow(() => { repository.Delete(1); });
        }
    }
}
