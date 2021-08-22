using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Models;

namespace BookBook.Repositories
{
    public interface IResetPasswordRepository
    {
        ResetPasswordRequest GetRequest(Guid id);
        ResetPasswordRequest GetRequest(Guid id, int code);
        void AddReset(ResetPasswordRequest reset);
        void DeleteReset(Guid id);
    }
}
