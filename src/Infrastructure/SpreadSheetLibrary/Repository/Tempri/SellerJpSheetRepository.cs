using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
namespace SpreadSheetLibrary.Repository.Tempri
{
    public class SellerJpSheetRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;
        private readonly string sheetId = "1Dg1WTkgyK2inNp2TonYC30j7tJydkDUFLEoAQsehDcU";
        private readonly string sheetName = "seller_jp";
        public SellerJpSheetRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();

        }
        public async Task<SellerJpSheetObject> GetSellerJpSheetObject(int printId)
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
                return new SellerJpSheetObject()
                {
                    PrintId = printId,
                    PrintName = String.Empty,
                    Asin = String.Empty,
                    Sku = String.Empty,
                    FnSku = String.Empty,
                    Keywords = String.Empty,
                    Description = String.Empty,
                };
            }
            else
            {
                return new SellerJpSheetObject()
                {
                    PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                    PrintName = value[header.IndexOf("PrintName")].ToString(),
                    Asin = value[header.IndexOf("Asin")].ToString(),
                    Sku = value[header.IndexOf("Sku")].ToString(),
                    FnSku = value[header.IndexOf("FnSku")].ToString(),
                    Keywords = value[header.IndexOf("Keywords")].ToString(),
                    Description = value[header.IndexOf("Description")].ToString(),
                };
            }
        }

    }
}
