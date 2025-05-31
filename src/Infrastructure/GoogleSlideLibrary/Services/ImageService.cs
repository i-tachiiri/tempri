using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using GoogleSlideLibrary.Config;
using TempriDomain.Interfaces;

namespace GoogleSlideLibrary.Services
{
    public class ImageService
    {
        private readonly SlidesConnecter slidesConnecter;
        private readonly SlidesService slideService;

        public ImageService(SlidesConnecter slidesConnecter)
        {
            this.slidesConnecter = slidesConnecter;
            slideService = slidesConnecter.GetSlidesService();
        }
        public async Task<string> GetTopImageId(string PresentationId, int PageNumber)
        {
            var presentation = slideService.Presentations.Get(PresentationId).Execute();
            return presentation.Slides[PageNumber].PageElements.Where(element => element.Image != null).OrderBy(element => element.Transform.TranslateY).ToList()[0].ObjectId;
        }
        public async Task<string> GetTopImageId(string PresentationId, int PageNumber, int index)
        {
            var presentation = slideService.Presentations.Get(PresentationId).Execute();
            return presentation.Slides[PageNumber].PageElements.Where(element => element.Image != null).OrderBy(element => element.Transform.TranslateY).ToList()[index].ObjectId;
        }
        public async Task<string> GetBottomImageId(string PresentationId, int PageNumber)
        {
            var presentation = slideService.Presentations.Get(PresentationId).Execute();
            return presentation.Slides[PageNumber].PageElements.Where(element => element.Image != null).OrderByDescending(element => element.Transform.TranslateY).ToList()[0].ObjectId;
        }
        public async Task<string> GetLeftImageId(string PresentationId, int PageNumber)
        {
            var presentation = slideService.Presentations.Get(PresentationId).Execute();
            return presentation.Slides[PageNumber].PageElements.Where(element => element.Image != null).OrderBy(element => element.Transform.TranslateX).ToList()[0].ObjectId;
        }
        public async Task<List<string>> GetSortedImageIds(string presentationId, int pageNumber)
        {
            var presentation = await slideService.Presentations.Get(presentationId).ExecuteAsync();

            return presentation.Slides[pageNumber]
                .PageElements
                .Where(element => element.Image != null)
                .OrderBy(element => element.Transform.TranslateY)  // Top to bottom
                .ThenBy(element => element.Transform.TranslateX)   // Left to right if same height
                .Select(element => element.ObjectId)
                .ToList();
        }
        public async Task<List<string>> GetSortedImageIds(Presentation presentation, int pageNumber)
        {
            return presentation.Slides[pageNumber]
                .PageElements
                .Where(element => element.Image != null)
                .OrderBy(element => element.Transform.TranslateY)  // Top to bottom
                .ThenBy(element => element.Transform.TranslateX)   // Left to right if same height
                .Select(element => element.ObjectId)
                .ToList();
        }
        public async Task<string> GetRightImageId(string PresentationId, int PageNumber)
        {
            var presentation = slideService.Presentations.Get(PresentationId).Execute();
            return presentation.Slides[PageNumber].PageElements.Where(element => element.Image != null).OrderByDescending(element => element.Transform.TranslateX).ToList()[0].ObjectId;
        }
        public async Task GetImagesReplaceRequests(string PresentationId, List<string> imagePaths)
        {
            var requests = new List<Request>();
            int UrlIndex = 0;
            int PageIndex = 0;
            var presentation = await slideService.Presentations.Get(PresentationId).ExecuteAsync();

            foreach (var page in presentation.Slides)
            {
                var objectIds = await GetSortedImageIds(presentation, PageIndex);

                foreach (var objectId in objectIds)
                {
                    var imageUrl = imagePaths[UrlIndex];
                    requests.Add(new Request
                    {
                        ReplaceImage = new ReplaceImageRequest
                        {
                            ImageObjectId = objectId,
                            ImageReplaceMethod = "CENTER_INSIDE",
                            Url = imageUrl
                        }
                    });
                    UrlIndex++;
                }
                PageIndex++;
            }
            var batchRequest = new BatchUpdatePresentationRequest
            {
                Requests = requests
            };
            await slideService.Presentations.BatchUpdate(batchRequest, PresentationId).ExecuteAsync();
        }

        public async Task GetImageReplaceRequest(string presentationId, string baseObjectId, string pngUrl)
        {
            var requests = new List<Request>
            {
                new Request
                {
                    ReplaceImage = new ReplaceImageRequest
                    {
                        ImageObjectId = baseObjectId,
                        ImageReplaceMethod = "CENTER_INSIDE",
                        Url = pngUrl
                    }
                }
            };

            var batchUpdateRequest = new BatchUpdatePresentationRequest
            {
                Requests = requests
            };

            await slideService.Presentations.BatchUpdate(batchUpdateRequest, presentationId).ExecuteAsync();
        }
   
        public async Task GetImageReplaceRequest(string presentationId, List<string> baseObjectIds, string pngUrl)
        {
            var requests = new List<Request>();

            foreach (var objectId in baseObjectIds)
            {
                requests.Add(new Request()
                {
                    ReplaceImage = new ReplaceImageRequest()
                    {
                        ImageObjectId = objectId,
                        ImageReplaceMethod = "CENTER_INSIDE",
                        Url = pngUrl
                    }
                });
            }

            var batchUpdateRequest = new BatchUpdatePresentationRequest
            {
                Requests = requests
            };

            await slideService.Presentations.BatchUpdate(batchUpdateRequest, presentationId).ExecuteAsync();
        }


    }
}
