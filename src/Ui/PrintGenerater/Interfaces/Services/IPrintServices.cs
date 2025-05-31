using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Services;

public interface IPrintFactory
{
  Task<IPrintTask> CreatePrintTask(IPrintMasterEntity master);
}

public interface IPrintController
{
  Task Execute(CancellationToken token);
}

public interface IPrintClassGetter
{
  Type GetPrintClass(string printId);
}

public interface IPrintImageInserter
{
  Task InsertImages(IPrintMasterEntity master);
}

public interface IPrintSlideRepository
{
  Task<string> GetPresentationId(IPrintMasterEntity master);
  Task SavePresentationId(IPrintMasterEntity master, string presentationId);
}

public interface IPrintMasterRepository
{
  Task<IPrintMasterEntity> GetMaster(string printId);
  Task SaveMaster(IPrintMasterEntity master);
}