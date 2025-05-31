namespace PrintGenerater.Interfaces.Infrastructure;

public interface IQrGenerator
{
  Task<byte[]> GenerateQrCode(string content, int size = 300);
  Task<string> GenerateAndSaveQrCode(string content, string filePath, int size = 300);
  Task<bool> ValidateQrCode(string content);
}