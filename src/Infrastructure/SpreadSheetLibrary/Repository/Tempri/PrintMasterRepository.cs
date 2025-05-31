using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json.Linq;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
using TempriDomain.Entity;
using TempriDomain.Interfaces;

namespace SpreadSheetLibrary.Repository.Tempri
{
    public class PrintMasterRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;
        private readonly string sheetId = "1YXfMMbu82ZZnPTi-SsfcXCDJYDMWMajLUs-f7Nj-obg";
        private readonly string TemplateQaSheetId = "1xqpvyg9KDVOXuCiy9q5HioJOwlPcHdOQu1K8a0OrOwI";
        private readonly string sheetName = "master";
        public PrintMasterRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();
        }
        public async Task<IList<IList<object>>> GetValues()
        {
            var range = sheetName;
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, range);
            var response = await request.ExecuteAsync();
            return response.Values;
        }
        public async Task UpdatePrintMasterObject(IPrintMasterEntity master)
        {
            var values = await GetValues();
            var header = values[0].Select(h => h.ToString()).ToList();
            int rowIndex = GetRowIndex(values, master.PrintId) + 1; 

            var type = typeof(PrintMasterObject);
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

        public async Task<PrintMasterObject> GetPrintMasterEntity(int printId)
        {
            var values = await GetValues();
            var header = values[0].Select(h => h.ToString()).ToList();

            var value = values
                .Skip(1)
                .FirstOrDefault(row => row[header.IndexOf("PrintId")].ToString() == printId.ToString());
            if (value == null)
            {
                return new PrintMasterObject()
                {
                    PrintId = printId + 1,
                    PrintName = String.Empty,
                    Language = String.Empty,
                    PagesCount = 0,
                    PrintCode = String.Empty,
                    QaSheetId = String.Empty,
                    PrintSlideId = String.Empty,
                    AmazonSlideId = String.Empty,
                    EtzySlideId = String.Empty,
                    Asin = String.Empty,
                    Sku = String.Empty,
                    FnSku = String.Empty,
                    Keywords = String.Empty,
                    Description = String.Empty,
                    etzy_en = "FALSE",
                    seller_jp = "FALSE",
                    pinkoi_en = "FALSE",
                };
            }
            else
            {
                for (int i = value.Count; i < header.Count; i++)
                {
                    value.Add(null); // 空文字で補完（必要なら "0" などにしてもOK）
                }
                return new PrintMasterObject()
                {
                    PrintId = int.Parse(value[header.IndexOf("PrintId")]?.ToString() ?? "0"),
                    PrintName = value[header.IndexOf("PrintName")]?.ToString() ?? string.Empty,
                    Language = value[header.IndexOf("Language")]?.ToString() ?? string.Empty,
                    PagesCount = int.Parse(value[header.IndexOf("PagesCount")]?.ToString() ?? "0"),
                    PrintCode = value[header.IndexOf("PrintCode")]?.ToString() ?? string.Empty,
                    QaSheetId = value[header.IndexOf("QaSheetId")]?.ToString() ?? string.Empty,
                    PrintSlideId = value[header.IndexOf("PrintSlideId")]?.ToString() ?? string.Empty,
                    AmazonSlideId = value[header.IndexOf("AmazonSlideId")]?.ToString() ?? string.Empty,
                    EtzySlideId = value[header.IndexOf("EtzySlideId")]?.ToString() ?? string.Empty,
                    Asin = value[header.IndexOf("Asin")]?.ToString() ?? string.Empty,
                    Sku = value[header.IndexOf("Sku")]?.ToString() ?? string.Empty,
                    FnSku = value[header.IndexOf("FnSku")]?.ToString() ?? string.Empty,
                    Keywords = value[header.IndexOf("Keywords")]?.ToString() ?? string.Empty,
                    Description = value[header.IndexOf("Description")]?.ToString() ?? string.Empty,
                    etzy_en = value[header.IndexOf("etzy_en")]?.ToString() ?? "FALSE",
                    seller_jp = value[header.IndexOf("seller_jp")]?.ToString() ?? "FALSE",
                    pinkoi_en = value[header.IndexOf("pinkoi_en")]?.ToString() ?? "FALSE"
                };
            }
        }
        public int GetRowIndex(IList<IList<object>> values,int PrintId)
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
        public async Task UpdateCellByIndex(string value,int rowIndex,int columnIndex)
        {
            string columnLetter = GetColumnLetter(columnIndex);
            string cellRef = $"{sheetName}!{columnLetter}{rowIndex}";

            var valueRange = new ValueRange
            {
                Values = new List<IList<object>> { new List<object> { value } }
            };

            var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, sheetId, cellRef);
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
