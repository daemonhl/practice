namespace GroupM.Content.Domain.Entities
{
    public class TextAnalysisResult
    {
        public int TotalNegativeWords { get; private set; }

        public TextAnalysisResult(int totalNegativeWords)
        {
            TotalNegativeWords = totalNegativeWords;
        }
    }
}
