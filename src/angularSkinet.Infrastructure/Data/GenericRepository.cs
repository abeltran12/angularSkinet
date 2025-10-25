using angularSkinet.Core.Entities;
using angularSkinet.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace angularSkinet.Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> 
    where T : BaseEntity
{
    private readonly StoreContext _context = context;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public bool Exists(int id)
    {
        return _context.Set<T>().Any(x => x.Id == id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(_context.Set<T>().AsQueryable(), spec);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = _context.Set<T>().AsQueryable();

        query = spec.ApplyCriteria(query);

        return await query.CountAsync();
    }
}
