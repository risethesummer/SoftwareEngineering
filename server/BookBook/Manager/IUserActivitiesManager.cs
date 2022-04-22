using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Manager
{
    public interface IUserActivitiesManager
    {
        Guid AddOnlineUser(Guid id);
        void OffUser(Guid sesId);
        bool IsOnline(Guid id);
        bool IsValidSession(Guid sesId);
        Guid GetUserId(Guid sesId);
    }
}
