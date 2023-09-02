using Application.Parameters;
using Domain.Enums;

namespace Application.DTOs;

public class DataGambarDTO
{
    public Kelas Kelas { get; set; }
    
    public double Red { get; init; }
    
    public double Green { get; init; }
    
    public double Blue { get; init; }
    
    public GlcmParams Glcm { get; init; }
}