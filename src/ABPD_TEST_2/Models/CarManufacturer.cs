using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.Models;

public class CarManufacturer
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(200)]
    public string Name { get; set; }
    
    public ICollection<Car> Cars { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}