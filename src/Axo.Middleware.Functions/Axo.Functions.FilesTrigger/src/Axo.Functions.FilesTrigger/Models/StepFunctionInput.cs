namespace Axo.Functions.FilesTrigger.Models;

public class StepFunctionInput
{
  public string FileKey { get; set; }
  public int Type { get; set; } // 1 = Inventory, 2 = Price
}