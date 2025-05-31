using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.Archive;
using SpreadSheetLibrary.DataTransferObject;
using TempriDomain.Interfaces;

namespace SpreadSheetLibrary.Repository.Tempri
{
    public class QaSheetRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;

        public QaSheetRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();
        }
        public async Task<string> GetColumnAString(string sheetId, string sheetName)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, $"{sheetName}!A1:A1000");
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;
            return string.Join("\n", values.Where(row => row.Count > 0).Select(row => row[0].ToString()));
        }
        public async Task<List<string>> GetColumnAStringList(string sheetId, string sheetName)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, $"{sheetName}!A1:A1000");
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;
            return values.Where(row => row.Count > 0).Select(row => row[0].ToString()).ToList();
        }
        public async Task<List<string>> GetTexOrder(string sheetId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, "order");
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;
            return values.Select(row => row[0].ToString()).ToList();
        }
        public async Task<List<IQuestionMasterEntity>> GetQaSheetObjectsAsync(string sheetId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, "qa");
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            if (values == null || values.Count < 2)
                return new List<IQuestionMasterEntity>();

            var header = values[0].Select(h => h.ToString()).ToList();
            var dataRows = values.Skip(1);

            var result = new List<IQuestionMasterEntity>();

            foreach (var row in dataRows)
            {
                var obj = new QaSheetObject();
                var type = typeof(QaSheetObject);

                foreach (var property in type.GetProperties())
                {
                    if (!property.CanWrite) continue;
                    string columnName = property.Name;
                    var valueString = GetPropertyValue(row, header, columnName);

                    if (property.PropertyType == typeof(int))
                    {
                        if (int.TryParse(valueString, out int intValue))
                        {
                            property.SetValue(obj, intValue);
                        }
                        else
                        {
                            property.SetValue(obj, 0); // 変換できない場合は0をセット
                        }
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(obj, valueString);
                    }
                    else
                    {
                        // それ以外の型は今は無視
                    }
                }
                result.Add(obj);
            }

            return result;
        }

        private string GetPropertyValue(IList<object> row, List<string> header, string columnName)
        {
            var index = header.IndexOf(columnName);
            return (index >= 0 && index < row.Count) ? row[index].ToString() : " ";
        }
        public async Task<Dictionary<string, List<string>>> GetTexTemplates(string qaSheetId, List<string> order)
        {
            var distinctOrder = order.Distinct().ToList();
            var templateTasks = distinctOrder.ToDictionary(
                sheetName => sheetName,
                sheetName => GetColumnAStringList(qaSheetId, sheetName)
            );
            await Task.WhenAll(templateTasks.Values);
            var texTemplates = new Dictionary<string, List<string>>();
            foreach (var pair in templateTasks)
            {
                texTemplates[pair.Key] = await pair.Value;
            }
            return texTemplates;
        }
        public string ReplacePlaceholder(IQuestionMasterEntity qaValue, string texTemplate)
        {
            var type = typeof(QaSheetObject);
            foreach (var property in type.GetProperties().OrderByDescending(x => x.Name))
            {
                string placeholder = property.Name;
                string value = property.GetValue(qaValue)?.ToString() ?? property.Name;
                texTemplate = texTemplate.Replace(placeholder, value);
            }

            return texTemplate;
        }
    }
}
