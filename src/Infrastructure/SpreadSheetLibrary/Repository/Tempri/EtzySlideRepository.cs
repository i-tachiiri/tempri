using CoincheckDomain.Config;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
using SpreadSheetLibrary.Services;


namespace SpreadSheetLibrary.Repository.Tempri
{
    public class EtzySlideRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;
        private readonly string sheetId = "1Dg1WTkgyK2inNp2TonYC30j7tJydkDUFLEoAQsehDcU";
        private readonly string sheetName = "slide_etzy";
        public EtzySlideRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();

        }
        public async Task<SlideEtzySheetObject> GetSlideEtzySheetObject(int printId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .FirstOrDefault(row => row[header.IndexOf("PrintId")].ToString() == printId.ToString());

            if (value == null)
            {
                return new SlideEtzySheetObject()
                {
                    PrintId = printId,
                    PrintName = "no print name",
                    PageCount = 0,
                    PresentationId = "no presentation",
                    PresentationUrl = "no presentation",
                };
            }
            else
            {
                return new SlideEtzySheetObject()
                {
                    PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                    PrintName = value[header.IndexOf("PrintName")].ToString(),
                    PageCount = int.Parse(value[header.IndexOf("PagesCount")].ToString()),
                    PresentationId = value[header.IndexOf("PresentationId")].ToString(),
                    PresentationUrl = value[header.IndexOf("PresentationUrl")].ToString(),
                };
            }
        }

    }
}
