namespace PrintGenerater.Interfaces.Services;
public interface IAmazonImageGenerator
{
  public Task GenerateImage(IPrintMasterEntity print);
  public Task ReplaceAnswerImage(IPrintMasterEntity print);
  public Task ReplaceAnswer4Image(IPrintMasterEntity print);
  public Task ReplaceQuestionImage(IPrintMasterEntity print);
  public Task ExportPngs(IPrintMasterEntity print);
}