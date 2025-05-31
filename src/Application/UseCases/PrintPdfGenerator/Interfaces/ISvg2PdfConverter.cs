using TempriDomain.Entity;
namespace PrintPdfGenerator.Interfaces;

public interface ISvg2PdfConverter
{
  public void CreateVectorPdf(PrintMasterEntity master);
}