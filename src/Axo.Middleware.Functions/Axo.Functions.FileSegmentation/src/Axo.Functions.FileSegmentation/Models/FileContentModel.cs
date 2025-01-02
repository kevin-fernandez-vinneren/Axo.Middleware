namespace Axo.Functions.FileSegmentation.Models;

public class FileContentModel<T>
{
  public int MerchantId { get; set; }
  public List<T> ListInfo { get; set; }
}