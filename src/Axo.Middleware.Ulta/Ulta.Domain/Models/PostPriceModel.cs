namespace Ulta.Domain.Models;

public class PostPriceModel
{
  public int MerchantId { get; set; }
  public string Upc { get; set; }
  public decimal Price { get; set; }
  public decimal BasePrice { get; set; }
  public bool IsGift { get; set; }
  public DateTime LoadDate { get; set; }
  public string Status { get; set; }
}