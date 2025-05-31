
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
using SpreadSheetLibrary.Services;
namespace SpreadSheetLibrary.Repository.Tempri
{
    public class ProductSheetRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;
        private readonly string sheetId = "1Dg1WTkgyK2inNp2TonYC30j7tJydkDUFLEoAQsehDcU";
        private readonly string sheetName = "product";
        public ProductSheetRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();

        }
        public async Task<ProductSheetObject> GetProductSheetObjectAsync(int PrintId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .First(row => row[header.IndexOf("PrintId")].ToString() == PrintId.ToString());

            return new ProductSheetObject()
            {
                PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                PrintCode = value[header.IndexOf("PrintCode")].ToString(),
                PrintName = value[header.IndexOf("PrintName")].ToString(),
                SheetId = value[header.IndexOf("SheetId")].ToString(),
                PagesCount = int.Parse(value[header.IndexOf("PagesCount")].ToString()),
                Language = value[header.IndexOf("Language")].ToString(),
                IsEtzyEn = value[header.IndexOf("etzy_en")].ToString() == "TRUE",
                IsSellerJp = value[header.IndexOf("seller_jp")].ToString() == "TRUE",
            };
        }
        public ProductSheetObject GetProductSheetObject(int PrintId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = request.Execute();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .First(row => row[header.IndexOf("PrintId")].ToString() == PrintId.ToString());

            return new ProductSheetObject()
            {
                PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                PrintCode = value[header.IndexOf("PrintCode")].ToString(),
                PrintName = value[header.IndexOf("PrintName")].ToString(),
                SheetId = value[header.IndexOf("SheetId")].ToString(),
                PagesCount = int.Parse(value[header.IndexOf("PagesCount")].ToString()),
                Language = value[header.IndexOf("Language")].ToString(),
                IsEtzyEn = value[header.IndexOf("etzy_en")].ToString() == "TRUE",
                IsSellerJp = value[header.IndexOf("seller_jp")].ToString() == "TRUE",
            };
        }
    }
}
