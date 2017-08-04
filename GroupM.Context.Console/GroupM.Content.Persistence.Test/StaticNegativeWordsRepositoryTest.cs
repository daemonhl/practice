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
    public class StaticNegativeWordsRepositoryTest
    {
        private IUnityContainer unityContainer;
        private INegativeWordsRepository repository;

        [OneTimeSetUp]
        public void Init()
        {

        }

        [SetUp]
        public void SetupTest()
        {
            repository = new StaticNegativeWordsRepository();
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldHavePredefinedData()
        {
            // Arrange

            // Act
            var predefinedItems = repository.GetAll();
            var numberOfItems = predefinedItems.Count();

            // Assert
            Assert.That(numberOfItems, Is.EqualTo(4));
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldReturnById()
        {
            // Arrange
            var expectedItem = new NegativeWord() { Id = 0, Text = "horrible" };

            // Act
            var item = repository.Get(0);

            // Assert
            Assert.IsTrue(expectedItem.Id == item.Id && expectedItem.Text == item.Text);
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldReturnNullIfDoesntExist()
        {
            // Arrange

            // Act & Assert
            Assert.IsNull(repository.Get(10));
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldAddAndReturnItem()
        {
            // Arrange
            var newItem = new NegativeWord() { Text = "terrible" };

            // Act
            repository.Add(newItem);
            var addedItem = repository.Get(newItem.Id);

            // Assert
            Assert.AreEqual(newItem, addedItem);
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldUpdateItem()
        {
            // Arrange
            var replacement = new NegativeWord() { Id = 0, Text = "creepy" };

            // Act
            repository.Update(replacement);
            var updatedItem = repository.Get(0);

            // Assert
            Assert.IsTrue(updatedItem.Id == replacement.Id && updatedItem.Text == replacement.Text);
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldReturnNullOnDeletedItem()
        {
            // Arrange

            // Act
            repository.Delete(0);

            // Assert
            Assert.IsNull(repository.Get(0));
        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldNotBeThrowingNotFound()
        {
            // Arrange

            // Act
            repository.Delete(0);

            // Assert
            Assert.DoesNotThrow(() => { repository.Delete(0); });
        }
    }
}
