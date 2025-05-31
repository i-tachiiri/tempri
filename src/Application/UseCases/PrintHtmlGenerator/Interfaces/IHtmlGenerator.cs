using TempriDomain.Entity;

namespace PrintHtmlGenerator.Interfaces;

public interface IHtmlGenerator
{
  void GenerateHtml(PrintMasterEntity master);
}