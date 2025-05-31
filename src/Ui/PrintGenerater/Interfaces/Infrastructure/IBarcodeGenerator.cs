namespace PrintGenerater.Interfaces.Infrastructure;

public interface IBarcodeGenerator
{
  Task<byte[]> GenerateBarcode(string content, int width = 300, int height = 100);
  Task<string> GenerateAndSaveBarcode(string content, string filePath, int width = 300, int height = 100);
  Task<bool> ValidateBarcode(string content);
}