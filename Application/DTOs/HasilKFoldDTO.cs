namespace Application.DTOs;

public record HasilKFoldDTO
{
    public IEnumerable<HasilKFold> HasilKFold { get; set; }

    public double RataRataAKurasiTotal { get; set; }
    
    public double RataRataAKurasiApelBaik { get; set; }
    
    public double RataRataAKurasiApelBuruk { get; set; }
}