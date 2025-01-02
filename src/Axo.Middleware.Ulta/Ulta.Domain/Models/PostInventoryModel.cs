namespace Ulta.Domain.Models;

public class PostInventoryModel
{
  public int MerchantId { get; set; }
  public string Upc { get; set; }
  public int Stock { get; set; }
  public DateTime LoadDate { get; set; }
  public string Status { get; set; }
}