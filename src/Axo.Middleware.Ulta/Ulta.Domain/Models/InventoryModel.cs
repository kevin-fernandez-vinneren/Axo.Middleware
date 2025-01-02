namespace Ulta.Domain.Models;

public class InventoryModel
{
  public string Sku { get; set; }
  public int Qty { get; set; }
  public int IsInStock { get; set; }
}