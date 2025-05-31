using TempriDomain.Entity;

public interface ITex2PdfConverter
{
  public Task Convert(PrintMasterEntity master);
}