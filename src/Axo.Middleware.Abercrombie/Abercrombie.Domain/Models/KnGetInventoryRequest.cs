namespace Abercrombie.Domain.Models;

public class KnGetInventoryRequest
{
  public InventoryRequestQuery Query { get; set; }
}

public class InventoryRequestQuery
{
  public InventoryRequestParameters Parametros {  get; set; }    
}
public class InventoryRequestParameters 
{
  public string TipoConsulta { get; set; }
  public string? Ean { get; set; }
  public int Ciclo { get; set; }
}