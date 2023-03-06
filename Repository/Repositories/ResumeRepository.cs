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
    public class ResumeRepository : BaseRepository<Resume>
    {
        public ResumeRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public IList<Resume> GetBySkills(IList<long> skillIds)
        {
            return this.table
                .Where(res => res.Skills.Any(skill => skillIds.Contains(skill.Id)))
                .Include(res => res.Skills)
                .ToList();
        }

        public Resume? GetById(long id)
        {
            return this.table
                .Where(resume => resume.Id == id)
                .Include(resume => resume.Skills)
                .FirstOrDefault();
        }

        public PageResult<Resume> GetAll(
            int pageNumber,
            int pageSize,
            string? keywords)
        {
            IQueryable<Resume> query;
            if (keywords != null && keywords.Trim().Length > 0)
            {
                query = this.table
                .Where(res => res.Name.Contains(keywords) || res.Email.Contains(keywords) || res.Phone.Contains(keywords))
                .OrderByDescending(resume => resume.CreatedAt);
            } else
            {
                query = this.table
                .OrderByDescending(resume => resume.CreatedAt);
            }
            

            int totalItems = query.Count();

            query = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(resume => resume.Skills);

            return new PageResult<Resume>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = query.ToList()
            };
        }
    }
}
