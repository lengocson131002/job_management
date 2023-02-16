using Repository.Models;
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
    }
}
