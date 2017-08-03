using GroupM.Content.Domain;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using Microsoft.Practices.Unity;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            var negativeWords = new Dictionary<int, NegativeWord>()
            {
                {1, new NegativeWord() { Id = 1, Text = "bad" } },
                {2, new NegativeWord() { Id = 2, Text = "horrible" } },
                {3, new NegativeWord() { Id = 3, Text = "nasty" } },
                {4, new NegativeWord() { Id = 4, Text = "swine" } }
            };

            negativeWordsRepository.Get(Arg.Is(1)).Returns(negativeWords[1]);
            negativeWordsRepository.Get(Arg.Is(2)).Returns(negativeWords[2]);
            negativeWordsRepository.Get(Arg.Is(3)).Returns(negativeWords[3]);
            negativeWordsRepository.Get(Arg.Is(4)).Returns(negativeWords[4]);

            negativeWordsRepository.GetAll().Returns(negativeWords.Values);

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

        [Test]
        public void TextAnalysisService_ShouldBeCountingEachInstanceOfNegativeWord()
        {
            // Arrange
            var userText = new UserText() { Id = 1, Text = "The bad weather is not so bad when you're in some horrible situation surrounded by some nasty people" };

            // Act
            var result = service.ProcessText(userText);

            // Assert
            Assert.That(result.TotalNegativeWords, Is.EqualTo(4));
        }

        [Test]
        public void TextAnalysisService_ShouldNotBeCountingPartialWords()
        {
            // Arrange
            var userText = new UserText() { Id = 1, Text = "This is a badass car" };

            // Act
            var result = service.ProcessText(userText);

            // Assert
            Assert.That(result.TotalNegativeWords, Is.EqualTo(0));
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            unityContainer.Dispose();
        }
    }
}
