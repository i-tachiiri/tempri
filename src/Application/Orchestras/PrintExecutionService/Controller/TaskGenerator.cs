using XeLatexLibrary;

namespace PrintExecutionService.Controller;

public class TaskGenerator(PdfGenerator pdfGenerator, PrintFactory printFactory, HtmlGenerator htmlGenerator,
    QrGenerator qrGenerator, BarcodeGenerator barcodeGenerator, CoverGenerator coverGenerator, EcBaseGenerator ecBaseGenerator,
    FtpUploader ftpUploader, ConsoleRepository consoleRepository, AmazonImageGenerator amazonImageGenerator,
    EtzyImageGenerator etzyImageGenerator, WorksheetBuilder worksheetBuilder, QaSheetOpener qaSheetOpener,
    WorksheetOpener worksheetOpener, TexGenerator texGenerator, PrintMasterGetter printMasterGetter,
    PrintImageInserter printImageInserter, TexGenerator testTexGenerator, TestSlideSetter testSlideSetter,
    SlideSetter slideSetter, WebpageOpener webpageOpener, TexTemplateSelector templateSelector, Tex2PdfConverter tex2PdfConverter,
    Pdf2PngConverter pdf2PngConverter, Pdf2SvgConverter pdf2SvgConverter, TexDeleter texDeleter, BaseSvgGenerator baseSvgGenerator,
    PdfSplitter pdfSplitter, StandaloneSvgConverter standaloneSvgConverter, TemplateResetter templateResetter,
    CoverWorksheetGenerator coverWorksheetGenerator, GroupPdfGenerator groupPdfGenerator, CoverSetter coverSetter,
    Svg2PdfConverter svg2PdfConverter, PdfCompiler pdfCompiler)

{
    public async Task GeneratePrintTask(int printId, string input)
    {
        var isTestCase = input.Contains("-t");
        var option = input.Replace("-t", "").Trim();

        var master = await printMasterGetter.GetPrintEntity(printId);

        switch (option)
        {
            case "setup":
                consoleRepository.TaskLog(templateResetter.Delete, master);
                await consoleRepository.TaskLog(worksheetBuilder.Build, master);
                consoleRepository.TaskLog(templateSelector.Select, master);
                break;
            case "qa":
                await consoleRepository.TaskLog(qaSheetOpener.OpenQaSheet, master);
                break;
            case "tex":
                consoleRepository.TaskLog(texDeleter.Delete, master);
                await consoleRepository.TaskLog(texGenerator.Generate, master, isTestCase);
                await consoleRepository.TaskLog(tex2PdfConverter.Convert, master);
                consoleRepository.TaskLog(pdfSplitter.Split, master);
                consoleRepository.TaskLog(pdf2SvgConverter.Convert, master);
                break;
            case "html":
                consoleRepository.TaskLog(htmlGenerator.GenerateHtml, master);
                break;
            case "upload":
                await consoleRepository.TaskLog(ftpUploader.UploadFiles, master);
                break;
            case "webpage":
                await consoleRepository.TaskLog(webpageOpener.OpenWorkSheet, master);
                break;
            case "pdf":
                consoleRepository.TaskLog(qrGenerator.ExportQrCodes, master);
                await consoleRepository.TaskLog(baseSvgGenerator.Generate, master, isTestCase);
                consoleRepository.TaskLog(barcodeGenerator.GenerateBarcodeSvg, master);
                consoleRepository.TaskLog(coverWorksheetGenerator.Generate, master);
                consoleRepository.TaskLog(coverSetter.Generate, master);
                consoleRepository.TaskLog(standaloneSvgConverter.Convert, master);
                consoleRepository.TaskLog(svg2PdfConverter.CreateVectorPdf, master);
                consoleRepository.TaskLog(groupPdfGenerator.CreateGroupedAnswerPdf, master);
                consoleRepository.TaskLog(pdfGenerator.CreateGoodsPdf, master);
                break;
            case "cover":
                consoleRepository.TaskLog(ecBaseGenerator.Convert2Pngs, master);
                await consoleRepository.TaskLog(amazonImageGenerator.GenerateImage, master);
                await consoleRepository.TaskLog(etzyImageGenerator.GenerateImage, master);
                break;
            default:
                consoleRepository.WriteLog($"option {option} does not exist.");
                break;
        }
    }

}