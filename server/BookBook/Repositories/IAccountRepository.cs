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
        bool CheckAccountExist(string account);
        bool UpdateAccount(UserAccount account);
        bool UpdateAccount(Guid id, byte[] newPassword);
        UserAccount GetAccount(string account, byte[] password);
        UserAccount GetAccount(Guid id);
        UserAccount GetAccount(string account, string email);
        IEnumerable<UserAccount> GetAccounts();
    }
}
