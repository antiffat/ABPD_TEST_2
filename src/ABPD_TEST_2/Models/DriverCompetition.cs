using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPD_TEST_2.Models;

public class DriverCompetition
{
    [Key, Column(Order = 0)]
    public int DriverId { get; set; }
    
    [Key, Column(Order = 1)]
    public int CompetitionId { get; set; }
    
    [Required]
    public DateTime Date  { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}