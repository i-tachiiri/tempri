using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Infrastructure;

public interface ISheetConnecter
{
  Task<List<List<string>>> ReadRange(string spreadsheetId, string range);
  Task UpdateRange(string spreadsheetId, string range, List<List<object>> values);
  Task AppendRange(string spreadsheetId, string range, List<List<object>> values);
  Task ClearRange(string spreadsheetId, string range);
  Task<string> CreateSpreadsheet(string title);
  Task DeleteSpreadsheet(string spreadsheetId);
  Task<bool> SheetExists(string spreadsheetId, string sheetName);
}