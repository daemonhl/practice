using GroupM.Content.Domain.Entities;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;

namespace GroupM.Content.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            IUnityContainer container = InitializeUnityContainer();

            var analysisService = container.Resolve<ITextAnalysisService>();
            var textsRepository = container.Resolve<IUserTextsRepository>();

            // choose a text
            var text = textsRepository.Get(1);

            // process the text
            var result = analysisService.ProcessText(text);

            ShowResult(text, result);
        }

        private static void ShowResult(UserText text, TextAnalysisResult result)
        {
            Console.WriteLine("Text:");
            Console.WriteLine(text.Text);
            Console.WriteLine("Total negative words: {0}", result.TotalNegativeWords);

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

        private static IUnityContainer InitializeUnityContainer()
        {
            return new UnityContainer().LoadConfiguration();
        }
    }
}
