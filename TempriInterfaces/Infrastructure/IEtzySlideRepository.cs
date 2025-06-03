using TempriDomain.Entity;

namespace TempriInterfaces.Infrastructure;

public interface IEtzySlideRepository
{
    Task<SlideEtzySheetObject> GetSlideEtzySheetObject(string printId);
}