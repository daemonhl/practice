using GroupM.Content.Domain;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using Microsoft.Practices.Unity;
using NSubstitute;
using NUnit.Framework;
using System;

namespace GroupM.Content.Domain.Test
{
    [TestFixture]
    public class TextAnalysisServiceTest
    {
        private IUnityContainer unityContainer;
        private ITextAnalysisService service;

        [OneTimeSetUp]
        public void Init()
        {
            // Initialize mocking
            var negativeWordsRepository = Substitute.For<INegativeWordsRepository>();
            negativeWordsRepository.Get(Arg.Is(1)).Returns(new NegativeWord() { Id = 1, Text = "bad" });
            negativeWordsRepository.Get(Arg.Is(2)).Returns(new NegativeWord() { Id = 2, Text = "horrible" });
            negativeWordsRepository.Get(Arg.Is(3)).Returns(new NegativeWord() { Id = 2, Text = "nasty" });
            negativeWordsRepository.Get(Arg.Is(4)).Returns(new NegativeWord() { Id = 2, Text = "swine" });

            // initialize Unity
            unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(negativeWordsRepository);
            unityContainer.RegisterType<ITextAnalysisService, TextAnalysisService>();
        }

        [SetUp]
        public void SetUpTest()
        {
            service = unityContainer.Resolve<ITextAnalysisService>();
        }

        [Test]
        public void TextAnalysisService_ShouldBeFindingNegativeWordsIfTheyArePresent()
        {
            // Arrange
            var userText = new UserText() { Id = 1, Text = "The weather in London in August is bad.Is like winter, horrible" };

            // Act
            var result = service.ProcessText(userText);

            // Assert
            Assert.That(result.TotalNegativeWords, Is.EqualTo(2));
        }

        [Test]
        public void TextAnalysisService_ShouldNotBeFindingNegativeWordsIfTheyAreNotPresent()
        {
            // Arrange
            var userText = new UserText() { Id = 1, Text = "The weather in London in August is very nice and warm compared to Alaska" };

            // Act
            var result = service.ProcessText(userText);

            // Assert
            Assert.That(result.TotalNegativeWords, Is.EqualTo(0));
        }

        [Test]
        public void TextAnalysisService_ShouldThrowNullReferenceWhenNullPassed()
        {
            // Arrange
            UserText nullUserText = null;

            // Act & Assert
            Assert.Throws(typeof(NullReferenceException), () => service.ProcessText(nullUserText));
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            unityContainer.Dispose();
        }
    }
}
