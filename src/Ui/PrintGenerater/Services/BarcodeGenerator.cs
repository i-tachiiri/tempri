using ZXing;
using ZXing.Common;
namespace PrintGenerater.Services;

public class BarcodeGenerator : IBarcodeGenerator
{
    public void GenerateBarcodeSvg(IPrintMasterEntity print)
    {
        var barcodeWriter = new BarcodeWriterSvg
        {
            Format = BarcodeFormat.CODE_128,
            Options = new EncodingOptions
            {
                Width = 300,
                Height = 100,
                Margin = 10,
                PureBarcode = true // ← ここで文字を非表示に
            }
        };

        var barcode = string.IsNullOrEmpty(print.FnSku) ? "example" : print.FnSku;
        var barcodeSvg = barcodeWriter.Write(barcode);

        // オプション：fill="black" を強制（安全策）
        string svgContent = barcodeSvg.Content
            .Replace("<path", "<path fill=\"black\"")
            .Replace("<rect", "<rect fill=\"black\"");



        var exportPath = Path.Combine(print.GetDirectory(print.PrintId, "cover-base"), "barcode.svg");
        File.WriteAllText(exportPath, svgContent);
    }
}
