using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.DTOs;

public class DriverDetailDto
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
    public int CarNumber { get; set; }
    
    [Required, MaxLength(200)]
    public string ManufacturerName { get; set; }
    
    [Required, MaxLength(200)]
    public string ModelName { get; set; }
}