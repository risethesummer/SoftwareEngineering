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

        public bool CheckLoginAccount(string account, string password)
        {
            byte[] hashPass = MD5.HashData(Encoding.ASCII.GetBytes(password));
            return dbContext.UserAccounts.FirstOrDefault<UserAccount>(acc => acc.Account == account && acc.Password == hashPass) != null;
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

        public IEnumerable<UserAccount> GetAccounts()
        {
            return dbContext.UserAccounts.AsEnumerable();
        }

        public void UpdateAccount(UserAccount account)
        {
            if (dbContext.UserAccounts.Find(account.ID) != null)
            {
                dbContext.UserAccounts.Update(account);
                dbContext.SaveChanges();
            }
        }
    }
}
