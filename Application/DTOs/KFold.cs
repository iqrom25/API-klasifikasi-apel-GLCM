using Domain.Enums;

namespace Application.DTOs;

public record KFold
{
    public int Id { get; set; }
    public Kelas Hasil { get; set; }
    public Kelas Aktual { get; set; }
    public bool Keterangan { get; set; }
    public double Jarak { get; set; }
};