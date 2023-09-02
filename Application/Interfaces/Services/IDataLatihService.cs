using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces.Services;

public interface IDataLatihService
{
    Task<IEnumerable<DataLatih>> GetAll();

    Task<DataLatih> Create(DataLatih data);

    Task DeleteAll();

    Task<HasilKFoldDTO> GetKFold(int fold);
}