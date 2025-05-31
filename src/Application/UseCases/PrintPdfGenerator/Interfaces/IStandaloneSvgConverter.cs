using TempriDomain.Entity;
namespace PrintPdfGenerator.Interfaces;

public interface IStandaloneSvgConverter
{
  public void Convert(PrintMasterEntity print);
}