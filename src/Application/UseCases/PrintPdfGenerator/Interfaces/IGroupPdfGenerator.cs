using TempriDomain.Entity;

namespace PrintPdfGenerator.Interfaces;

public interface IGroupPdfGenerator
{
  public void CreateGroupedAnswerPdf(PrintMasterEntity print);
}