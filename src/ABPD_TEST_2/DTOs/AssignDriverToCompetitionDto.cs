using System.ComponentModel.DataAnnotations;

namespace ABPD_TEST_2.DTOs;

public class AssignDriverToCompetitionDto
{
    [Key]
    public int DriverId { get; set; }
    
    [Key]
    public int CompetitionId { get; set; }
}