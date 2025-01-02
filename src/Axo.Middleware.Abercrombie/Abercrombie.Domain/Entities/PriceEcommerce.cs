using System.ComponentModel.DataAnnotations.Schema;

namespace Abercrombie.Domain.Entities;

public class PriceEcommerce
{
  public string Upc { get; set; }
  
  [Column("Precio_Lista")]
  public decimal PrecioLista { get; set; }
  
  [Column("Precio_Final")]
  public decimal PrecioFinal { get; set; }

  public bool Regalo { get; set; }

  [Column("Fecha_Carga_Md")]
  public DateTime FechaCargaMd { get; set; }

  [Column("Id_Ecom")]
  public int IdEcom { get; set; }
  
  [Column("Tienda_Ecom")]
  public string TiendaEcom { get; set; }

  public string Flag { get; set; }

  public DateTime Dttm { get; set; }
}