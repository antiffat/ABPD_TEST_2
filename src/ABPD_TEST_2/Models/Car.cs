using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CarManufacturerId { get; set; }
    
    public CarManufacturer CarManufacturer { get; set; }
    
    [Required, MaxLength(200)]
    public string ModelName { get; set; }
    
    [Required]
    public int Number { get; set; }
    
    public ICollection<Driver> Drivers { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}