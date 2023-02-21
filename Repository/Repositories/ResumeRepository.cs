using Microsoft.EntityFrameworkCore;
using Repository.Models;
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
    }
}
