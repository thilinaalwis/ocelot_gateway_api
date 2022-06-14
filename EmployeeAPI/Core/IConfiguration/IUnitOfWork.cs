using EmployeeAPI.Core.IRepositories;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }

        Task CompleteAsync();
    }
}
