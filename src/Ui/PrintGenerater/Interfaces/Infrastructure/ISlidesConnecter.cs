using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Infrastructure;

public interface ISlidesConnecter
{
  Task<string> CreatePresentation(string title);
  Task<string> DuplicatePresentation(string sourceId, string title);
  Task DeletePresentation(string presentationId);
  Task<int> GetSlideCount(string presentationId);
  Task UpdateSlideContent(string presentationId, Dictionary<string, string> replacements);
  Task ExportPdf(string presentationId, string outputPath);
}