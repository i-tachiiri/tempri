namespace PrintGenerater.Interfaces.Services;

public interface IBarcodeGenerator
{
  public void GenerateBarcodeSvg(IPrintMasterEntity print);
}