
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using GoogleSlideLibrary.Config;

namespace GoogleSlideLibrary.Services
{
    public class TableService
    {
        private readonly SlidesConnecter slidesConnecter;
        private readonly SlidesService slideService;
        private readonly PresentationService presentationService;
        public TableService(SlidesConnecter slidesConnecter, PresentationService presentationService)
        {
            this.slidesConnecter = slidesConnecter;
            slideService = slidesConnecter.GetSlidesService();
            this.presentationService = presentationService;
        }
        public PageElement GetTable(Presentation presentation,int pageIndex,int tableIndex)
        {
            var page = presentation.Slides[pageIndex];
            var tableElements = page.PageElements.Where(element => element.Table != null).ToList();
            return tableElements.OrderBy(element => element.Transform.TranslateY).ToList()[tableIndex];     
        }
        public List<Request> GetUpdateTextRequest(PageElement element, string value,int RowNumber,int ColumnNumber)
        {
            var requests = new List<Request>();
            requests.Add(InsertTableText(element, RowNumber, ColumnNumber, " "));
            requests.Add(DeleteTableText(element, RowNumber, ColumnNumber));
            requests.Add(InsertTableText(element, RowNumber, ColumnNumber, value));
            return requests;
        }
        private Request DeleteTableText(PageElement element, int RowNumber, int ColumnNumber)
        {
            var deleteRequest = new Request();
            deleteRequest.DeleteText = new DeleteTextRequest();
            deleteRequest.DeleteText.ObjectId = element.ObjectId;
            deleteRequest.DeleteText.CellLocation = new TableCellLocation();
            deleteRequest.DeleteText.CellLocation.RowIndex = RowNumber;
            deleteRequest.DeleteText.CellLocation.ColumnIndex = ColumnNumber;
            deleteRequest.DeleteText.TextRange = new Google.Apis.Slides.v1.Data.Range();
            deleteRequest.DeleteText.TextRange.Type = "ALL";
            return deleteRequest;
        }
        private Request InsertTableText(PageElement element, int RowNumber, int ColumnNumber, string insertText)
        {
            var insertRequest = new Request();
            insertRequest.InsertText = new InsertTextRequest();
            insertRequest.InsertText.ObjectId = element.ObjectId;
            insertRequest.InsertText.Text = insertText;
            insertRequest.InsertText.CellLocation = new TableCellLocation();
            insertRequest.InsertText.CellLocation.RowIndex = RowNumber;
            insertRequest.InsertText.CellLocation.ColumnIndex = ColumnNumber;
            return insertRequest;
        }

    }
}
