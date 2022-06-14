using EmployeeAPI.Core.IRepositories;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext context,ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Employee>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(EmployeeRepository));
                return new List<Employee>();
            }
        }

        public override async Task<bool> Update(Employee entity)
        {
            try
            {
                var existingEmployee = await dbSet.Where(x => x.Id == entity.Id)
                                                        .FirstOrDefaultAsync();

                if (existingEmployee == null)
                    return await Add(entity);

                existingEmployee.FirstName = entity.FirstName;
                existingEmployee.LastName = entity.LastName;
                existingEmployee.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(EmployeeRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(EmployeeRepository));
                return false;
            }
        }
    }
}
