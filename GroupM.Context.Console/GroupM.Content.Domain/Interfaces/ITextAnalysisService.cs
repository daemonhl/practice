using GroupM.Content.Domain.Entities;
using GroupM.Content.Entities;

namespace GroupM.Content.Domain.Interfaces
{
    public interface ITextAnalysisService
    {
        TextAnalysisResult ProcessText(UserText text);
    }
}
