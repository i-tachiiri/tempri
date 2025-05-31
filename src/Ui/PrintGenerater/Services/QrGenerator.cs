using QRCoder;
using System.Text.RegularExpressions;
namespace PrintGenerater.Services.Setup;

public class QrGenerator
{
    public void ExportQrCodes(IPrintMasterEntity print)
    {
        foreach (var worksheet in print.worksheets)
        {
            ExportAnswerQrCodes(worksheet);
            ExportQuestionQrCodes(worksheet);
        }
    }
    private void ExportAnswerQrCodes(IWorksheetMasterEntity worksheet)
    {
        var PrintType = "a";
        var url = $"{worksheet.GetUrlWithFolder(worksheet, "html")}/{worksheet.print.PrintCode}-{worksheet.worksheetNumber.ToString("D3")}-{PrintType}.html";
        var folder = "qr-a";
        GenerateSvgQrCode(worksheet, url, folder);
    }
    private void ExportQuestionQrCodes(IWorksheetMasterEntity worksheet)
    {
        var PrintType = "q";
        var url = $"{worksheet.GetUrlWithFolder(worksheet, "html")}/{worksheet.print.PrintCode}-{worksheet.worksheetNumber.ToString("D3")}-{PrintType}.html";
        var folder = "qr-q";
        GenerateSvgQrCode(worksheet, url, folder);
    }
    private void GenerateSvgQrCode(IWorksheetMasterEntity worksheet, string url, string folder)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new SvgQRCode(qrCodeData);

        int dotSize = 10;
        int moduleCount = qrCodeData.ModuleMatrix.Count;
        int qrSize = moduleCount * dotSize;

        int textHeight = 80;
        string viewBox = $"0 0 {qrSize} {qrSize + textHeight}";

        string svg = qrCode.GetGraphic(dotSize, "#000000", "white", drawQuietZones: false);  // ← 背景を white に変更

        svg = Regex.Replace(svg, @"viewBox=""[^""]+""", $"viewBox=\"{viewBox}\"", RegexOptions.IgnoreCase);
        svg = Regex.Replace(svg, @"<path(?![^>]*\sfill=)", "<path fill=\"black\"", RegexOptions.IgnoreCase);
        svg = Regex.Replace(svg, @"<rect(?![^>]*\sfill=)", "<rect fill=\"black\"", RegexOptions.IgnoreCase);

        var exportPath = worksheet.GetFilePathWithExtension(worksheet, folder, "svg");
        File.WriteAllText(exportPath, svg);
    }



}
