using CoincheckDomain.Config;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
using SpreadSheetLibrary.Services;
using TempriDomain.Interfaces;


namespace SpreadSheetLibrary.Repository.Tempri
{
    public class AmazonSlideRepository
    {
        private readonly SheetCrudService sheetCrudService;
        private readonly SheetsService sheetsService;

        private readonly string sheetId = "1Dg1WTkgyK2inNp2TonYC30j7tJydkDUFLEoAQsehDcU";
        private readonly string sheetName = "slide_amazon";
        public AmazonSlideRepository(SheetCrudService sheetCrudService, SheetsService sheetsService)
        {
            this.sheetCrudService = sheetCrudService;
            this.sheetsService = sheetsService;
        }
        public async Task<string> GetPresentationId(string printId)
        {
            var values = await sheetCrudService.GetValuesAsync(sheetId, sheetName);
            foreach (var row in values)
            {
                if (row.Count >= 4 && row[0]?.ToString() == printId)
                {
                    return row[3]?.ToString(); // Column D = index 3
                }
            }
            return null;
        }
        public async Task<SlideAmazonSheetObject> GetPrintSheetObject(int PrintId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .First(row => row[header.IndexOf("PrintId")].ToString() == PrintId.ToString());

            return new SlideAmazonSheetObject()
            {
                PrintId = int.Parse(value[header.IndexOf("PrintId")].ToString()),
                PrintName = value[header.IndexOf("PrintName")].ToString(),
                PresentationId = value[header.IndexOf("PresentationId")].ToString(),
                PresentationUrl = value[header.IndexOf("PresentationUrl")].ToString()
            };
        }
        public async Task<IList<IList<object>>> GetValues()
        {
            var range = sheetName;
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, range);
            var response = await request.ExecuteAsync();
            return response.Values;
        }
        public int GetRowIndex(IList<IList<object>> values, int PrintId)
        {
            var PrintIds = values.Skip(1).Select(row => int.Parse(row[0].ToString())).ToList<int>();
            var rowIndex = PrintIds.Any(id => id == PrintId) ? PrintIds.IndexOf(PrintId) + 1 : PrintIds.Max() + 1;
            return rowIndex;
        }
        public int GetColumnIndex(IList<IList<object>> values, string columnName)
        {
            var columnNames = values[0].Select(column => column.ToString()).ToList();
            var columnIndex = columnNames.Any(column => column == columnName) ? columnName.IndexOf(columnName) : columnNames.Count + 1;
            return columnIndex;
        }
        public async Task UpdateSlideAmazonObject(IPrintMasterEntity master)
        {
            var values = await GetValues();
            var header = values[0].Select(h => h.ToString()).ToList();
            int rowIndex = GetRowIndex(values, master.PrintId) + 1;

            var type = typeof(SlideAmazonSheetObject);
            var properties = type.GetProperties().ToDictionary(p => p.Name, p => p);
            var rowData = new List<object>();
            foreach (var columnName in header)
            {
                if (properties.TryGetValue(columnName, out var prop))
                {
                    var rawValue = prop.GetValue(master);
                    string value = rawValue switch
                    {
                        bool b => b ? "TRUE" : "FALSE",
                        _ => rawValue?.ToString() ?? ""
                    };
                    rowData.Add(value);
                }
                else
                {
                    rowData.Add("");
                }
            }
            string endColumnLetter = GetColumnLetter(header.Count - 1);
            string range = $"{sheetName}!A{rowIndex}:{endColumnLetter}{rowIndex}";

            var valueRange = new ValueRange
            {
                Values = new List<IList<object>> { rowData }
            };

            var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, sheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            await updateRequest.ExecuteAsync();
        }
        private string GetColumnLetter(int columnIndex)
        {
            string column = "";
            while (columnIndex >= 0)
            {
                column = (char)('A' + (columnIndex % 26)) + column;
                columnIndex = columnIndex / 26 - 1;
            }
            return column;
        }
    }
}
