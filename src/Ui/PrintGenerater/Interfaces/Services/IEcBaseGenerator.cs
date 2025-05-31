public interface IEcBaseGenerator
{
  public void Convert2Pngs(IPrintMasterEntity print);
  public void ConvertSvgWithAdaptiveResize(string svgPath, string pngPath);
  public void ConvertPdfWithAdaptiveResize(string pdfPath, string pngPath);
  public void ExportQuestionPng(string svgPath, string pngPath);
  public void ExportAnswerPng(string pdfPath, string pngPath);
  public void ExportSvgToPng(string svgPath, string pngPath, uint size);
  public void ExportPdfToPng(string pdfPath, string pngPath, uint size = 3000);
}