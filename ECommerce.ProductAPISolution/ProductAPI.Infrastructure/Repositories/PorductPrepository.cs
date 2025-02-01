using eCommerce.SharedLibrary.Logs;
using eCommerce.SharedLibrary.Responses;
using ProductAPI.Application.Interfaces;
using ProductAPI.Domain.Entities;
using ProductAPI.Infrastructure.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ProductAPI.Infrastructure.Repositories;

public class PorductPrepository : IProductRepository
{
    private readonly ProductDbContext _db;

    public PorductPrepository(ProductDbContext db)
    {
        _db = db;
    }

    public async Task<Response> CreateAsync(Product entity)
    {
        var transaction = _db.Database.BeginTransaction();
        try
        {
            var existing = await GetAsync(p => p.Name.Equals(entity.Name));
            if (existing != null) {
                return new Response
                {
                    Flag = false,
                    Message = $"{entity.Name} is already existed",
                };
            }

            var product = _db.Products.Add(entity).Entity;
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response
            {
                Flag = true,
                Message = $"{entity.Name} added successfully"
            };
        }
        catch (Exception ex) { 
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            transaction.Rollback();
            return new Response
            {
                Message = ex.Message,
                Flag = false,
            };
        }
    }

    public async Task<Response> DeleteAsync(Product entity)
    {
        var transaction = _db.Database.BeginTransaction();
        try
        {
            var existing = await GetByID(entity.Id);
            if (existing == null)
            {
                return new Response
                {
                    Flag = false,
                    Message = $"{entity.Name} is not existed",
                };
            }

            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response
            {
                Flag = true,
                Message = $"{entity.Name} deleted successfully"
            };
        }
        catch (Exception ex)
        {
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            transaction.Rollback();
            return new Response
            {
                Message = ex.Message,
                Flag = false,
            };
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var products = await _db.Products.AsNoTracking().ToListAsync();
            return products;
        }
        catch (Exception ex)
        {
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var product = await _db.Products.FirstOrDefaultAsync(predicate);
            return product;
        }
        catch (Exception ex)
        {
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Product> GetByID(int id)
    {
        try
        {
            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            return existing is not null ? existing : null!;
        }
        catch (Exception ex)
        {
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Response> UpdateAsync(Product entity)
    {
        var transaction = _db.Database.BeginTransaction();
        try
        {
            var existing = await GetByID(entity.Id);
            if (existing == null)
            {
                return new Response
                {
                    Flag = false,
                    Message = $"{entity.Name} is not existed",
                };
            }

            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response
            {
                Flag = true,
                Message = $"{entity.Name} updated"
            };
        }
        catch (Exception ex)
        {
            //log original exception
            LogException.LogExceptions(ex, ex.Message);
            transaction.Rollback();
            return new Response
            {
                Message = ex.Message,
                Flag = false,
            };
        }
    }
}
