using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
namespace SpreadSheetLibrary.Repository.Tempri
{
    public class SlidePrintSheetRepository
    {
        private readonly SheetConnecter sheetConnecter;
        private readonly SheetsService sheetsService;
        private readonly string sheetId = "1Dg1WTkgyK2inNp2TonYC30j7tJydkDUFLEoAQsehDcU";
        private readonly string sheetName = "slide_print";
        public SlidePrintSheetRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();

        }
        public async Task<SlidePrintSheetObject> GetPrintSheetObjectAsync(int PrintId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .First(row => row[header.IndexOf("PrintId")].ToString() == PrintId.ToString());

            return new SlidePrintSheetObject()
            {
                PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                PrintName = value[header.IndexOf("PrintName")].ToString(),
                PageCount = int.Parse(value[header.IndexOf("PagesCount")].ToString()),
                PresentationId = value[header.IndexOf("PresentationId")].ToString(),
                PresentationUrl = value[header.IndexOf("PresentationUrl")].ToString()
            };
        }
        public SlidePrintSheetObject GetPrintSheetObject(int PrintId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = request.Execute();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .First(row => row[header.IndexOf("PrintId")].ToString() == PrintId.ToString());

            return new SlidePrintSheetObject()
            {
                PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                PrintName = value[header.IndexOf("PrintName")].ToString(),
                PageCount = int.Parse(value[header.IndexOf("PagesCount")].ToString()),
                PresentationId = value[header.IndexOf("PresentationId")].ToString(),
                PresentationUrl = value[header.IndexOf("PresentationUrl")].ToString()
            };
        }
    }
}
