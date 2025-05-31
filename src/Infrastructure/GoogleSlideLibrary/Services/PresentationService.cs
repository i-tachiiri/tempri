using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using GoogleSlideLibrary.Config;
namespace GoogleSlideLibrary.Services
{
    public class PresentationService
    {
        private readonly SlidesConnecter slidesConnecter;
        private readonly SlidesService slideService;

        public PresentationService(SlidesConnecter slidesConnecter)
        {
            this.slidesConnecter = slidesConnecter;
            slideService = slidesConnecter.GetSlidesService();
        }
        public async Task<Presentation> GetPresentation(string presentationId)
        {
            return await slideService.Presentations.Get(presentationId).ExecuteAsync();
        }
        public async Task BatchUpdate(List<Request> requests,string presentationId)
        {
            var batchUpdateRequest = new BatchUpdatePresentationRequest{Requests = requests};
            await slideService.Presentations.BatchUpdate(batchUpdateRequest, presentationId).ExecuteAsync();
        }
    }
}
