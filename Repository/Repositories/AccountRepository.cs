using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using Repository.Repositories.interfaces;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public Account? GetByUsername(String username)
        {
            if (username == null)
            {
                return null;
            }

            return table.Where<Account>(a => a.Username == username).FirstOrDefault();
        }

        public PageResult<Account> GetAll(
            string? query,
            AccountStatus? status,
            int pageNumber,
            int pageSize
            )
        {

            IQueryable<Account> tableQuery = this.table
                .Where(e => string.IsNullOrEmpty(query) ||
                e.Username.ToLower().Contains(query.ToLower()) ||
                e.FullName.ToLower().Contains(query.ToLower()) ||
                (e.Email != null &&
                e.Email.ToLower().Contains(query.ToLower())) || 
                (e.Phone != null &&
                e.Phone.ToLower().Contains(query.ToLower())))
                .Where(e => status == null || e.Status == status)
                .OrderByDescending(e => e.CreatedAt);

            var totalItems = tableQuery.Count();

            tableQuery = tableQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            return new PageResult<Account>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = tableQuery.ToList()
            };
        }

    }
}
