using TempriDomain.Entity;
using PrintTexGenerator.Interfaces;
public class TexPrintTask(IConsoleRepository console,ITexDeleter texDeleter,ITexGenerator texGenerator,ITex2PdfConverter tex2PdfConverter,
    IPdfSplitter pdfSplitter,IPdf2SvgConverter pdf2SvgConverter) : IPrintTask
{
    public string OptionKey => "tex";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(texDeleter.Delete, master);
        await console.TaskLog(texGenerator.Generate, master, isTestCase);
        await console.TaskLog(tex2PdfConverter.Convert, master);
        console.TaskLog(pdfSplitter.Split, master);
        console.TaskLog(pdf2SvgConverter.Convert, master);
    }
}
