using GroupM.Content.Entities;
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

        [OneTimeSetUp]
        public void Init()
        {

        }

        [Test]
        public void StaticNegativeWordsRepository_ShouldHavePredefinedData()
        {
            // Arrange
            var repository = new StaticNegativeWordsRepository();

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
            var repository = new StaticNegativeWordsRepository();
            var expectedItem = new NegativeWord() { Id = 0, Text = "horrible" };

            // Act
            var item = repository.Get(0);

            // Assert
            Assert.IsTrue(expectedItem.Id == item.Id && expectedItem.Text == item.Text);
        }
    }
}
