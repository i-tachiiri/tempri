using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Infrastructure;

public interface IImageService
{
  Task<string> SaveImage(byte[] imageData, string fileName);
  Task<byte[]> LoadImage(string filePath);
  Task<bool> ResizeImage(string inputPath, string outputPath, int width, int height);
  Task<bool> ConvertToPng(string inputPath, string outputPath);
  Task DeleteImage(string filePath);
  Task<bool> ImageExists(string filePath);
}