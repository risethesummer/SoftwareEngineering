using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Manager
{
    public interface IResetPasswordManager
    {
        bool AddRequest(Guid id, string email);
        bool ConfirmMailCode(Guid id, int mailCode);
    }
}
