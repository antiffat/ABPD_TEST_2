using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.Models;

public class Driver
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(200)]
    public string FirstName { get; set; }
    
    [Required, MaxLength(200)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthday { get; set; }
    
    [Required]
    public int CarId { get; set; }
    
    public Car Car { get; set; }
    
    public ICollection<DriverCompetition> DriverCompetitions { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}