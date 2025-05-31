using TempriDomain.Entity;

namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IEtzySlideRepository
{
    Task<SlideEtzySheetObject> GetSlideEtzySheetObject(string printId);
}