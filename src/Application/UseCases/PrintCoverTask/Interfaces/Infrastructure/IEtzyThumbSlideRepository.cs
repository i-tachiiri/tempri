using TempriDomain.Entity;

namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IEtzyThumbSlideRepository
{
    Task<SlideEtzySheetObject> GetSlideEtzySheetObject(string printId);
}