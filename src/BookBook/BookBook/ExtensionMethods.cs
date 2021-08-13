using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Dtos.Account;
using BookBook.Models;

namespace BookBook
{
    public static class ExtensionMethods
    {
        public static UserAccountDto AsDto (this UserAccount user)
        {
            return new UserAccountDto()
            { 
                ID = user.ID,
                Account = user.Account,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                DayOfBirth = user.DayOfBirth
            };
        }
    }
}
