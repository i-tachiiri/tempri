public interface IEtzyImageGenerator
{
  public Task GenerateImage(IPrintMasterEntity print);
  public Task ReplaceAnswerImage(IPrintMasterEntity print);
  public Task ReplaceThumbAnswerImage(IPrintMasterEntity print);
  public Task ReplaceQuestionImage(IPrintMasterEntity print);
  public Task ExportItemPngs(IPrintMasterEntity print);
  public Task ExportThumbPngs(IPrintMasterEntity print);
}