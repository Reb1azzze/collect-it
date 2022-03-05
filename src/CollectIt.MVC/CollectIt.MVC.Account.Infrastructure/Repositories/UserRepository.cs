using System.Data;
using CollectIt.MVC.Account.Abstractions;
using CollectIt.MVC.Account.IdentityEntities;
using CollectIt.MVC.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CollectIt.MVC.Account.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostgresqlIdentityDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(PostgresqlIdentityDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<int> AddAsync(User user)
    {
        try
        {
            return ( await _context.Users.AddAsync(user) ).Entity.Id;
        }
        catch (DbUpdateException db)
        {
            
        }
        catch (PostgresException postgres)
        {
            
        }
        catch (ConstraintException constraintException)
        {
            
        }
    }

    public Task RemoveAsync(User item)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User item)
    {
        throw new NotImplementedException();
    }
}