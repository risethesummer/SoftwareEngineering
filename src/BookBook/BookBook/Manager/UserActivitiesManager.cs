using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace BookBook.Manager
{
    public class UserActivitiesManager : IUserActivitiesManager
    {
        //User ID: The last time they activate
        public readonly Dictionary<Guid, DateTime> onlineUsers = new();

        public void ForceLogOutUser()
        {
            while (true)
            {
                Queue<Guid> shouldDelete = new();

                foreach (var user in onlineUsers)
                {
                    //The last activity is last 1 hour
                    if ((DateTime.Now - user.Value).TotalHours > 1)
                    {
                        shouldDelete.Enqueue(user.Key);
                    }
                }

                while (shouldDelete.Count > 0)
                    onlineUsers.Remove(shouldDelete.Dequeue());

                //Wait for 1 minutes to check again
                Thread.Sleep(60000);
            }
        }

        public void UserActive(Guid id)
        {
            if (onlineUsers.ContainsKey(id))
                onlineUsers[id] = DateTime.Now;
        }

        public bool AddOnlineUser(Guid id)
        {
            //The user has logged in
            if (onlineUsers.ContainsKey(id))
            {
                return false;
            }
            onlineUsers.Add(id, DateTime.Now);
            return true;
        }

        public void OffUser(Guid id)
        {
            onlineUsers.Remove(id);
        }

        public bool IsOnline(Guid id)
        {
            return onlineUsers.ContainsKey(id);
        }
    }
}
