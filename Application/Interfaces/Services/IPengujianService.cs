using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IPengujianService
{
    Task<HasilPengujianDTO> HasilPengujian(PengujianDTO dataUji);
}