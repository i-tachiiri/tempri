using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Services;

public interface ISlidesService
{
  Task<string> CreateSlide(string title);
  Task<string> DuplicateSlide(string sourceId, string title);
  Task UpdateSlideContent(string presentationId, Dictionary<string, string> replacements);
}

public interface ISlideSetter
{
  Task SetSlide(IPrintMasterEntity master);
}

public interface ITestSlideSetter
{
  Task SetTestSlide(IPrintMasterEntity master);
}

public interface IPresentationService
{
  Task<string> CreatePresentation(string title);
  Task<string> DuplicatePresentation(string sourceId, string title);
  Task UpdatePresentation(string presentationId, Dictionary<string, string> replacements);
}

public interface IEtzySlideRepository
{
  Task<string> GetPresentationId(string productId);
  Task SavePresentationId(string productId, string presentationId);
}

public interface IEtzyThumbSlideRepository
{
  Task<string> GetThumbPresentationId(string productId);
  Task SaveThumbPresentationId(string productId, string presentationId);
}

public interface ISlidePrintSheetRepository
{
  Task<List<string>> GetPrintIds();
  Task SavePrintIds(List<string> printIds);
}