using System.ComponentModel.DataAnnotations.Schema;

namespace Axo.Functions.Save.Entities;

public class InventoryEcommerce
{
  public string Upc { get; set; }
  
  [Column("Cantidad_Disponible")]
  public int CantidadDisponible { get; set; }
  
  [Column("Fecha_Carga_Md")]
  public DateTime FechaCargaMd { get; set; }
  
  [Column("Fecha_Carga_Dhw")]
  public DateTime FechaCargaDhw { get; set; }
  
  [Column("Id_Ecom")]
  public int IdEcom { get; set; }
  
  [Column("Tienda_Ecom")]
  public string TiendaEcom { get; set; }
  
  public string Flag { get; set; }
  
  public DateTime Dttm { get; set; }
}