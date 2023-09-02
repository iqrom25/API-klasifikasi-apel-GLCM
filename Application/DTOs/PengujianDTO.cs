using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class PengujianDTO
{
    public IFormFile Gambar { get; set; }
    
    public Sudut Sudut { get; set; }
}