using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.Interfaces.Services;

public interface IPelatihanService
{
    Task<IEnumerable<DataLatih>> Training(PelatihanDTO dataLatih);
}