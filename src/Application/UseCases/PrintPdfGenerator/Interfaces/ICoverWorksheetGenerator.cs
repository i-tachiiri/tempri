using TempriDomain.Entity;
namespace PrintPdfGenerator.Interfaces;

public interface ICoverWorksheetGenerator
{
  public void Generate(PrintMasterEntity print);
}