using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContractRepository : BaseRepository<Contract>
    {
        public ContractRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public PageResult<Contract> GetAll(
            long? companyId,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Contract> query = this.table
                .Where(contract => companyId == null || contract.CompanyId == companyId)
                .OrderByDescending(contract => contract.CreatedAt);

            int totalItems = query.Count();

            query = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(contract => contract.Company)
                .Include(contract => contract.Resume);

            return new PageResult<Contract>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = query.ToList()
            };

        }

        public Contract? FindByCompanyIdAndResumeId(long companyId, long resumeId)
        {
            return this.table
                .Where(contract => contract.CompanyId == companyId && contract.ResumeId == resumeId)
                .FirstOrDefault();
        }
    }
}
