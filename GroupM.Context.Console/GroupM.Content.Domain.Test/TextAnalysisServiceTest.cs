using GroupM.Content.Domain;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using Microsoft.Practices.Unity;
using NSubstitute;
using NUnit.Framework;
using System;

namespace GroupM.Practice.NegativeWords.LogicTests
{
    [TestFixture]
    public class TextAnalysisServiceTest
    {
        private IUnityContainer unityContainer;

        [OneTimeSetUp]
        public void Init()
        {
            // Initialize mocking
            var negativeWordsRepository = Substitute.For<INegativeWordsRepository>();
            negativeWordsRepository.Get(Arg.Is(1)).Returns(new NegativeWord() { Id = 1, Text = "bad" });
            negativeWordsRepository.Get(Arg.Is(2)).Returns(new NegativeWord() { Id = 2, Text = "horrible" });

            // initialize Unity
            unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(negativeWordsRepository);
            unityContainer.RegisterType<ITextAnalysisService, TextAnalysisService>();
        }

        [Test]
        public void ShouldThrowNotImplementedException()
        {
            // Arrange
            var service = unityContainer.Resolve<ITextAnalysisService>();
            var userText = new UserText() { Id = 1, Text = "Should be throwing NotImplementedException" };

            // Act & Assert
            Assert.Throws(typeof(NotImplementedException), () =>
            {
                var result = service.ProcessText(userText);
            });
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            unityContainer.Dispose();
        }
    }
}
