﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SkillRepository : BaseRepository<Skill>
    {
        public SkillRepository(JobManagementDBContext _context) : base(_context)
        {
        }
    }
}