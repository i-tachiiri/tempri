using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.Archive;

namespace SpreadSheetLibrary.Repository.Tempri
{
    public class _QaSheetRepository
    {
        private readonly SheetsService sheetsService;
        private readonly SheetConnecter sheetConnecter;
        private readonly string sheetName = "qa";

        public _QaSheetRepository(SheetConnecter sheetConnecter)
        {
            this.sheetConnecter = sheetConnecter;
            sheetsService = sheetConnecter.GetSheetsService();

        }
        public async Task<List<_QaSheetObject>> GetQaSheetObjectsAsync(string sheetId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            if (values == null || values.Count < 2)
                return new List<_QaSheetObject>();

            var header = values[0].Select(h => h.ToString()).ToList();
            var dataRows = values.Skip(1);

            var result = new List<_QaSheetObject>();

            foreach (var row in dataRows)
            {
                var obj = new _QaSheetObject
                {
                    Question = GetValue(row, header, "Question"),
                    Answer = GetValue(row, header, "Answer"),
                    MidStep1 = GetValue(row, header, "MidStep1"),
                    MidStep2 = GetValue(row, header, "MidStep2"),
                    MidStep3 = GetValue(row, header, "MidStep3"),
                    MidStep4 = GetValue(row, header, "MidStep4"),
                    MidStep5 = GetValue(row, header, "MidStep5"),
                    MidStep6 = GetValue(row, header, "MidStep6"),
                    MidStep7 = GetValue(row, header, "MidStep7"),
                    MidStep8 = GetValue(row, header, "MidStep8"),
                    MidStep9 = GetValue(row, header, "MidStep9"),
                    MidStep10 = GetValue(row, header, "MidStep10"),
                    MidStep11 = GetValue(row, header, "MidStep11"),
                    MidStep12 = GetValue(row, header, "MidStep12"),
                    MidStep13 = GetValue(row, header, "MidStep13"),
                    MidStep14 = GetValue(row, header, "MidStep14"),
                    MidStep15 = GetValue(row, header, "MidStep15"),
                    MidStep16 = GetValue(row, header, "MidStep16"),
                    MidStep17 = GetValue(row, header, "MidStep17"),
                    MidStep18 = GetValue(row, header, "MidStep18"),
                    MidStep19 = GetValue(row, header, "MidStep19"),
                    MidStep20 = GetValue(row, header, "MidStep20"),
                };

                result.Add(obj);
            }

            return result;
        }
        public List<_QaSheetObject> GetQaSheetObjects(string sheetId)
        {
            var request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = request.Execute();
            var values = response.Values;

            if (values == null || values.Count < 2)
                return new List<_QaSheetObject>();

            var header = values[0].Select(h => h.ToString()).ToList();
            var dataRows = values.Skip(1);

            var result = new List<_QaSheetObject>();

            foreach (var row in dataRows)
            {
                var obj = new _QaSheetObject
                {
                    Question = GetValue(row, header, "Question"),
                    Answer = GetValue(row, header, "Answer"),
                    MidStep1 = GetValue(row, header, "MidStep1"),
                    MidStep2 = GetValue(row, header, "MidStep2"),
                    MidStep3 = GetValue(row, header, "MidStep3"),
                    MidStep4 = GetValue(row, header, "MidStep4"),
                    MidStep5 = GetValue(row, header, "MidStep5"),
                    MidStep6 = GetValue(row, header, "MidStep6"),
                    MidStep7 = GetValue(row, header, "MidStep7"),
                    MidStep8 = GetValue(row, header, "MidStep8"),
                    MidStep9 = GetValue(row, header, "MidStep9"),
                    MidStep10 = GetValue(row, header, "MidStep10"),
                    MidStep11 = GetValue(row, header, "MidStep11"),
                    MidStep12 = GetValue(row, header, "MidStep12"),
                    MidStep13 = GetValue(row, header, "MidStep13"),
                    MidStep14 = GetValue(row, header, "MidStep14"),
                    MidStep15 = GetValue(row, header, "MidStep15"),
                    MidStep16 = GetValue(row, header, "MidStep16"),
                    MidStep17 = GetValue(row, header, "MidStep17"),
                    MidStep18 = GetValue(row, header, "MidStep18"),
                    MidStep19 = GetValue(row, header, "MidStep19"),
                    MidStep20 = GetValue(row, header, "MidStep20"),
                };

                result.Add(obj);
            }

            return result;
        }
        private string GetValue(IList<object> row, List<string> header, string columnName)
        {
            var index = header.IndexOf(columnName);
            return (index >= 0 && index < row.Count) ? row[index].ToString() : " ";
        }


    }
}
