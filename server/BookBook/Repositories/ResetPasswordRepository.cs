using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class ResetPasswordRepository : IResetPasswordRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ResetPasswordRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddReset(ResetPasswordRequest reset)
        {
            dbContext.ResetPasswordRequests.Add(reset);
            dbContext.SaveChanges();
        }

        public void DeleteReset(Guid id)
        {
            dbContext.UserAccounts.Remove(dbContext.UserAccounts.Find(id));
            dbContext.SaveChanges();
        }

        public ResetPasswordRequest GetRequest(Guid id)
        {
            return dbContext.ResetPasswordRequests.Find(id);
        }

        public ResetPasswordRequest GetRequest(Guid id, int code)
        {
            return dbContext.ResetPasswordRequests.FirstOrDefault(res => res.UserId == id && res.Code == code);
        }
    }
}
