using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace BookBook.Manager
{
    public class UserActivitiesManager
    {
        //User ID: The last time they activate
        public readonly Dictionary<Guid, DateTime> onlineUsers = new();

        public UserActivitiesManager()
        {
        }

        public void ForceLogOutUser()
        {
            List<Guid> shouldDelete = new();
            foreach (var user in onlineUsers)
            {
                //The last activity is last 1 hour
                if ((DateTime.Now - user.Value).TotalHours > 1)
                {
                    shouldDelete.Add(user.Key);
                }
            }

            foreach (var del in shouldDelete)
                onlineUsers.Remove(del);

            Thread.Sleep(60000);
        }
    }
}
