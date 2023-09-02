namespace Application.DTOs;

public record PerbandinganPengujian
{
    public int DataUji { get; init; }
    
    public int DataLatih { get; init; }
}