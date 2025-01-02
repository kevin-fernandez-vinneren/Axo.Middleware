namespace Abercrombie.Domain.Models;

public class CreationFileModel<T>
{
  public int MerchantId { get; set; }
  public List<T> ListInfo { get; set; }
}