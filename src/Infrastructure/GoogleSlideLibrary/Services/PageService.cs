using Google.Apis.Slides.v1.Data;
using GoogleSlideLibrary.Config;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Slides.v1;
using TempriDomain.Archive;
namespace GoogleSlideLibrary.Services
{
    public class PageService : IPageService
    {
        private readonly SlidesConnecter slidesConnecter;

        public PageService(SlidesConnecter slidesConnecter)
        {
            this.slidesConnecter = slidesConnecter;
        }
        public async Task<IList<Page>> GetPages(string presentationId)
        {
            // Get the Slides API service
            var slidesService = slidesConnecter.GetSlidesService();

            // Retrieve the presentation
            var request = slidesService.Presentations.Get(presentationId);
            var presentation = await request.ExecuteAsync();
            return presentation.Slides;

        }
        /// <summary>
        /// Get the PageObjectId from Google Slides by PageIndex
        /// </summary>
        /// <param name="presentationId">Google Slides Presentation ID</param>
        /// <param name="pageIndex">Index of the page (0-based)</param>
        /// <returns>PageObjectId as a string</returns>
        public async Task<List<string>> GetPageObjectIds(string presentationId)
        {
            try
            {
                // Get the Slides API service
                var slidesService = slidesConnecter.GetSlidesService();

                // Retrieve the presentation
                var request = slidesService.Presentations.Get(presentationId);
                var presentation = await request.ExecuteAsync();

                // Return the PageObjectId of the requested page
                return presentation.Slides.Select(slide => slide.ObjectId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching PageObjectId: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetPageObjectIdByIndex(string presentationId, int pageIndex)
        {
            try
            {
                // Get the Slides API service
                var slidesService = slidesConnecter.GetSlidesService();

                // Retrieve the presentation
                var request = slidesService.Presentations.Get(presentationId);
                var presentation = await request.ExecuteAsync();

                // Validate the page index
                if (presentation.Slides == null || presentation.Slides.Count <= pageIndex || pageIndex < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageIndex), "Invalid page index.");
                }

                // Return the PageObjectId of the requested page
                return presentation.Slides[pageIndex].ObjectId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching PageObjectId: {ex.Message}");
                return null;
            }
        }
        public async Task RemoveAfterPage2(string presentationId)
        {
            var slidesService = slidesConnecter.GetSlidesService();
            var request = slidesService.Presentations.Get(presentationId);
            var presentation = await request.ExecuteAsync();
            var pages = presentation.Slides;
            List<Request> requests = new List<Request>();
            for (int i = 1; i >= pages.Count; i++)
            {
                requests.Add(new Request
                {
                    DeleteObject = new DeleteObjectRequest
                    {
                        ObjectId = presentation.Slides[i].ObjectId
                    }
                });
            }
        }
        public async Task SyncPageNumber(string presentationId,int desiredPageCount)
        {
            var slidesService = slidesConnecter.GetSlidesService();
            var request = slidesService.Presentations.Get(presentationId);
            var presentation = await request.ExecuteAsync();
            int currentPageCount = presentation.Slides.Count;

            List<Request> requests = new List<Request>();

            if (currentPageCount < desiredPageCount)
            {
                // スライドが不足している場合、1枚目のスライドをコピー
                string slideObjectId = presentation.Slides[0].ObjectId;
                for (int i = currentPageCount; i < desiredPageCount; i++)
                {
                    requests.Add(new Request
                    {
                        DuplicateObject = new DuplicateObjectRequest
                        {
                            ObjectId = slideObjectId
                        }
                    });
                }
            }
            if (currentPageCount > desiredPageCount)
            {
                // スライドが多い場合、後ろから削除
                for (int i = currentPageCount - 1; i >= desiredPageCount; i--)
                {
                    requests.Add(new Request
                    {
                        DeleteObject = new DeleteObjectRequest
                        {
                            ObjectId = presentation.Slides[i].ObjectId
                        }
                    });
                }
            }

            if (requests.Count > 0)
            {
                var batchUpdateRequest = new BatchUpdatePresentationRequest { Requests = requests };
                slidesService.Presentations.BatchUpdate(batchUpdateRequest, presentationId).Execute();
            }
        }

    }
}
