using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models;

[Table("m_data_latih")]
public record DataLatih
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    [Required]
    public Kelas Kelas { get; init; }
    [Required]
    public double Red { get; init; }
    [Required]
    public double Green { get; init; }
    [Required]
    public double Blue { get; init; }
    [Required]
    public double Energi { get; init; }
    [Required]
    public double Kontras { get; init; }
    [Required]
    public double Homogenitas { get; init; }
    [Required]
    public double Korelasi { get; init; }
    
    [Required]
    public Sudut Sudut { get; init; }
};