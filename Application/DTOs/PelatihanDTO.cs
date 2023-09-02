using System;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class PelatihanDTO
{
    public IEnumerable<IFormFile> DataLatih { get; set; }

    public Kelas JenisPelatihan { get; set; }

    public Sudut Sudut { get; set; }
}