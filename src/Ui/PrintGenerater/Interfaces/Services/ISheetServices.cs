using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Services;

public interface ISheetCrudService
{
  Task<List<List<string>>> ReadRange(string spreadsheetId, string range);
  Task UpdateRange(string spreadsheetId, string range, List<List<object>> values);
  Task AppendRange(string spreadsheetId, string range, List<List<object>> values);
}

public interface ISheetsService
{
  Task<string> CreateSpreadsheet(string title);
  Task<List<List<string>>> ReadRange(string spreadsheetId, string range);
  Task UpdateRange(string spreadsheetId, string range, List<List<object>> values);
}

public interface IProductSheetRepository
{
  Task<List<string>> GetProductIds();
  Task SaveProductIds(List<string> productIds);
}

public interface IWorksheetOpener
{
  Task<string> OpenWorksheet(string title);
  Task<string> GetWorksheetId(string title);
}

public interface ITableService
{
  Task<List<List<string>>> GetTableData(string spreadsheetId, string range);
  Task UpdateTableData(string spreadsheetId, string range, List<List<object>> values);
}