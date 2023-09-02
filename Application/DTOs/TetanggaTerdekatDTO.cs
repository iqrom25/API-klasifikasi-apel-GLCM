using Application.Parameters;
using Domain.Models;

namespace Application.DTOs;

public class TetanggaTerdekatDTO
{
    public DataLatih DataLatih { get; init; }

    public double Jarak { get; set; }
}