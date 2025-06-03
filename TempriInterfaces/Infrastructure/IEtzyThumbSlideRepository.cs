using TempriDomain.Entity;

namespace TempriInterfaces.Infrastructure;

public interface IEtzyThumbSlideRepository
{
    Task<SlideEtzySheetObject> GetSlideEtzySheetObject(string printId);
}