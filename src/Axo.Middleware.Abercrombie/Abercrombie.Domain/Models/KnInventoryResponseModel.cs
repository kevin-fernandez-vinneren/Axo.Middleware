namespace Abercrombie.Domain.Models;

public class KnInventoryResponseModel
{
  public string Message { get; set; }
  public InventoryResults Results { get; set; }
  public string Status { get; set; }
}

public class InventoryResults
{
  public Stock Stock { get; set; }
  public InventoryItems Articulos { get; set; }
}

public class Stock
{
  public string Cliente { get; set; }
  public int FinCiclo { get; set; }
  public int Ciclo { get; set; }
}
public class InventoryItems
{
  public List<InventoryItem> Articulo { get; set; }
}

public class InventoryItem
{
  public string Ean { get; set; }
  public string? Descripcion { get; set; }
  public int Stock { get; set; }
  public int StockReservado { get; set; }
  public int StockPendiente { get; set; }
  public int StockBloquead { get; set; }
  public int StockTotal { get; set; }
}