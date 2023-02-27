using Repository.Models;
using Repository.Utils;
using Repository.Repositories.interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public Company? GetByName(String name)
        {
            if (name == null)
            {
                return null;
            }

            return table.Where(a => a.Name == name).FirstOrDefault();
        }

        public Company? GetByEmail(String email)
        {
            if (email == null)
            {
                return null;
            }

            return table.Where(a => a.Email == email).FirstOrDefault();
        }

        public PageResult<Company> GetAll(
            string? query,
            int pageNumber,
            int pageSize
            )
        {

            IQueryable<Company> tableQuery = this.table
                .Where(e => string.IsNullOrEmpty(query) || e.Name.ToLower().Contains(query.ToLower()))
                .OrderByDescending(e => e.CreatedAt);

            var totalItems = tableQuery.Count();

            tableQuery = tableQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.JobDescriptions);
            

            return new PageResult<Company>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = tableQuery.ToList()
            };
        }
    }
}
