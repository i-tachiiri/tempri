using TempriDomain.Entity;

namespace PrintPdfGenerator.Interfaces;

public interface ICoverSetter
{
  public void Generate(PrintMasterEntity print);
}