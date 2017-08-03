using GroupM.Content.Domain.Entities;
using GroupM.Content.Domain.Interfaces;
using GroupM.Content.Entities;
using GroupM.Content.Persistence.Interfaces;
using System;

namespace GroupM.Content.Domain
{
    public class TextAnalysisService : ITextAnalysisService
    {
        private INegativeWordsRepository negativeWordsRepository;

        public TextAnalysisService(INegativeWordsRepository negativeWordsRepository)
        {
            this.negativeWordsRepository = negativeWordsRepository;
        }

        public TextAnalysisResult ProcessText(UserText text)
        {
            throw new NotImplementedException();
        }
    }
}
