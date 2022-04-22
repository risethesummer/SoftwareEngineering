using System.Collections.Generic;
using System.Linq;
using System;

namespace BookBook.Manager
{
    public class UserActivitiesManager : IUserActivitiesManager
    {
        //SessionID: UserID
        public readonly Dictionary<Guid, Guid> onlineUsers = new();

        public Guid AddOnlineUser(Guid id)
        {
            Guid sesId = onlineUsers.FirstOrDefault(p => p.Value == id).Key;
            onlineUsers.Remove(sesId);
            System.Guid newSesId = Guid.NewGuid();
            onlineUsers.Add(newSesId, id);
            return newSesId;
        }

        public void OffUser(Guid sesId)
        {
            onlineUsers.Remove(sesId);
        }

        public bool IsOnline(Guid id)
        {
            return onlineUsers.ContainsValue(id);
        }

        public bool IsValidSession(Guid sesId)
        {
            return onlineUsers.ContainsKey(sesId);
        }

        public Guid GetUserId(Guid sesId)
        {
            return onlineUsers[sesId];
        }
    }
}
