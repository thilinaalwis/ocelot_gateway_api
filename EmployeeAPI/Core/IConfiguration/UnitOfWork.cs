using EmployeeAPI.Core.IRepositories;
using EmployeeAPI.Core.Repositories;
using EmployeeAPI.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.IConfiguration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EmployeeDbContext _context;
        private readonly ILogger _logger;

        public IEmployeeRepository Employees { get; private set; }

        public UnitOfWork(
            EmployeeDbContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Employees = new EmployeeRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
