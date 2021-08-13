using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookBook.Data;

namespace BookBook.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckAccountExist(string account)
        {
            return dbContext.UserAccounts.FirstOrDefault<UserAccount>(acc => acc.Account == account) != null;
        }

        public bool CreateAccount(UserAccount account)
        {
            if (dbContext.UserAccounts.FirstOrDefault(a => a.Account == account.Account) != null)
                return false;
            dbContext.UserAccounts.Add(account);
            dbContext.SaveChanges();
            return true;
        }

        public UserAccount GetAccount(Guid id)
        {
            return dbContext.UserAccounts.Find(id);
        }

        public UserAccount GetAccount(string account, string email)
        {
            return dbContext.UserAccounts.FirstOrDefault(acc => acc.Account == account && acc.Email == email);
        }

        public UserAccount GetAccount(string account, byte[] password)
        {
            return dbContext.UserAccounts.FirstOrDefault(acc => acc.Account == account && acc.Password == password);
        }

        public IEnumerable<UserAccount> GetAccounts()
        {
            return dbContext.UserAccounts.AsEnumerable();
        }

        public bool UpdateAccount(UserAccount account)
        {
            var user = dbContext.UserAccounts.Find(account.ID);
            if (user != null)
            {
                user.Name = account.Name;
                user.Email = account.Email;
                user.DayOfBirth = account.DayOfBirth;
                user.Address = account.Address;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateAccount(Guid id, byte[] newPassword)
        {
            var user = dbContext.UserAccounts.Find(id);
            if (user != null)
            {
                user.Password = newPassword;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
