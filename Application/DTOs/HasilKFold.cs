namespace Application.DTOs;

public record HasilKFold
{
    public IEnumerable<KFold> Detail { get; set; }
    public double AkurasiTotal { get; set; }
    public double AkurasiApelBaik { get; set; }
    public double AkurasiApelBuruk { get; set; }
}