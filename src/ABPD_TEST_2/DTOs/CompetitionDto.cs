using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.DTOs;

public class CompetitionDto
{
    [Required, MaxLength(200)]
    public string CompetitionName { get; set; }
    
    [Required]
    public DateTime CompetitionDate { get; set; }
}