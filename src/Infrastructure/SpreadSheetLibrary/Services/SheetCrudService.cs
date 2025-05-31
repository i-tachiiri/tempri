using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using SpreadSheetLibrary.Config;

namespace SpreadSheetLibrary.Services
{
    public class SheetCrudService
    {
        private readonly SheetsService sheetsService;
        public SheetCrudService(SheetConnecter sheetConnecter)
        {
            sheetsService = sheetConnecter.GetSheetsService();
        }
        public async Task<IList<IList<object>>> GetValuesAsync(string sheetId,string sheetName)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            return response.Values;
        }
    }
}
