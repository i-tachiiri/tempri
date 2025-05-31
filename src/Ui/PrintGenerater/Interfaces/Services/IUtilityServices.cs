using TempriDomain.Entity;

namespace PrintGenerater.Interfaces.Services;

public interface IAuthorityService
{
  Task<bool> Authorize();
  Task<bool> ValidateToken();
}

public interface IExportService
{
  Task Export(IPrintMasterEntity master);
}

public interface IQrAttacher
{
  Task AttachQr(IPrintMasterEntity master, string qrContent);
}

public interface IDeleteService
{
  Task Delete(IPrintMasterEntity master);
}

public interface IDuplicateService
{
  Task<IPrintMasterEntity> Duplicate(IPrintMasterEntity master);
}

public interface IInkscapeExporter
{
  Task<string> ExportPng(string svgPath, string outputPath);
  Task<string> ExportPdf(string svgPath, string outputPath);
}

public interface IPdfCompiler
{
  Task<string> CompilePdf(List<string> pdfPaths, string outputPath);
}