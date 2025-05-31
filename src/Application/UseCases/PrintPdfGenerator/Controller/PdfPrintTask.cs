using PrintPdfGenerator.Interfaces;
using TempriDomain.Entity;
namespace PrintPdfGenerator.Controller;

public class PdfPrintTask(
    IQrGenerator qrGenerator,
    IBaseSvgGenerator baseSvgGenerator,
    IBarcodeGenerator barcodeGenerator,
    ICoverWorksheetGenerator coverWorksheetGenerator,
    ICoverSetter coverSetter,
    IStandaloneSvgConverter standaloneSvgConverter,
    ISvg2PdfConverter svg2PdfConverter,
    IGroupPdfGenerator groupPdfGenerator,
    IPdfGenerator pdfGenerator,
    IConsoleRepository console
) : IPrintTask
{
    public string OptionKey => "pdf";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(qrGenerator.ExportQrCodes, master);
        await console.TaskLog(baseSvgGenerator.Generate, master, isTestCase);
        console.TaskLog(barcodeGenerator.GenerateBarcodeSvg, master);
        console.TaskLog(coverWorksheetGenerator.Generate, master);
        console.TaskLog(coverSetter.Generate, master);
        console.TaskLog(standaloneSvgConverter.Convert, master);
        console.TaskLog(svg2PdfConverter.CreateVectorPdf, master);
        console.TaskLog(groupPdfGenerator.CreateGroupedAnswerPdf, master);
        console.TaskLog(pdfGenerator.CreateGoodsPdf, master);
    }
}
