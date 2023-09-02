using System.Diagnostics;
using Application.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T:class
{

    private readonly ApplicationDbContext _db;
    private DbSet<T> _dbSet;
    public GenericRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }
    
    public async Task<T> Save(T entity)
    {
        try
        {
            var entry = await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entry.Entity;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<T>> FindAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Truncate(string table)
    {
        await _db.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {table}");
    }
}