using GroupM.Content.Domain.Entities;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using System.Text.RegularExpressions;

namespace GroupM.Content.Domain
{
    public class RegexBasedTextAnalysisService : ITextAnalysisService
    {
        private INegativeWordsRepository negativeWordsRepository;

        public RegexBasedTextAnalysisService(INegativeWordsRepository negativeWordsRepository)
        {
            this.negativeWordsRepository = negativeWordsRepository;
        }

        public TextAnalysisResult ProcessText(UserText text)
        {
            var badWordsCount = 0;
            var negativeWordsCollection = negativeWordsRepository.GetAll();

            foreach (var bannedWord in negativeWordsCollection)
            {
                var matches = Regex.Matches(text.Text, string.Format("\\s*{0}([,.:;\\s]|$)", bannedWord.Text));

                badWordsCount += matches.Count;
            }

            return new TextAnalysisResult(badWordsCount);
        }
    }
}
