using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.DTOs;

public class CreateDriverDto
{
    [Required, MaxLength(200)]
    public string FirstName { get; set; }
    
    [Required, MaxLength(200)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthday { get; set; }
    
    [Required]
    public int CarId { get; set; }
}