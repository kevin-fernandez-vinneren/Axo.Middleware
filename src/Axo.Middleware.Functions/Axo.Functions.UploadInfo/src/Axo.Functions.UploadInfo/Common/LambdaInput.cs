namespace Axo.Functions.UploadInfo.Common;

public class LambdaInput
{
  public string FileKey { get; set; }
  public int Type { get; set; } // 1 = Inventory, 2 = Price
}