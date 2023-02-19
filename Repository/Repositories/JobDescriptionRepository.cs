using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class JobDescriptionRepository : BaseRepository<JobDescription>
    {
        public JobDescriptionRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public PageResult<JobDescription> GetAll(
            string? query,
            long? companyId,
            JobLevel? level,
            JobType? type,
            long? skillId,
            int pageNumber,
            int pageSize
            )
        {

            IQueryable<JobDescription> tableQuery = this.table
                .Where(e => string.IsNullOrEmpty(query) || e.Title.ToLower().Contains(query.ToLower()))
                .Where(e => level == null || e.Level == level)
                .Where(e => type == null || e.Type == type)
                .Where(e => companyId == null || e.CompanyId == companyId)
                .Where(e => skillId == null || e.Skills.Any(s => s.Id == skillId))
                .OrderByDescending(e => e.CreatedAt);

            var totalItems = tableQuery.Count();

            tableQuery = tableQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.Skills)
                .Include(x => x.Company);


            return new PageResult<JobDescription>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = tableQuery.ToList()
            };
        }

        public JobDescription? GetById(long id)
        {
            return this.table
                .Where(job => job.Id == id)
                .Include(job => job.Company)
                .Include(job => job.Skills)
                .FirstOrDefault();
        }
    }
}
