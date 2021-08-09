using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Models;

namespace BookBook.Repositories
{
    public interface IAccountRepository
    {
        bool CreateAccount(UserAccount account);
        bool CheckLoginAccount(string userName, string password);
        bool CheckAccountExist(string account);
        void UpdateAccount(UserAccount account);
        UserAccount GetAccount(Guid id);
        UserAccount GetAccount(string account, string email);
        IEnumerable<UserAccount> GetAccounts();
    }
}
