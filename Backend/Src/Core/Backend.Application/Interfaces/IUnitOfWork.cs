using System.Threading.Tasks;

namespace Backend.Application.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}