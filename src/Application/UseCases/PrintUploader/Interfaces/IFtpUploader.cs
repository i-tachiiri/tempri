using TempriDomain.Entity;

namespace PrintUploader.Interfaces;

public interface IFtpUploader
{
  public Task UploadFiles(PrintMasterEntity printEntity);
}