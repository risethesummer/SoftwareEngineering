using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookBook.Dtos.Account;
using BookBook.Models;
using System.Security.Cryptography;
using System.Text;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("account")]
    public class UserAccountController : ControllerBase
    {
        private readonly IAccountRepository repository;

        public UserAccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public ActionResult CreateAccount(CreateAccountDto createDto)
        {
            if (!repository.CheckAccountExist(createDto.Account))
            {
                var newUser = new UserAccount()
                {
                    ID = new Guid(),
                    Account = createDto.Account,
                    Email = createDto.Email,
                    Address = createDto.Address,
                    DayOfBirth = createDto.DayOfBirth,
                    Name = createDto.Name,
                    Password = MD5.HashData(Encoding.ASCII.GetBytes(createDto.Password))
                };

                repository.CreateAccount(newUser);

                return CreatedAtAction("CreateAccount", newUser);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public ActionResult LoginAccount(LoginAccountDto loginDto)
        {
            if (repository.CheckLoginAccount(loginDto.Account, loginDto.Password))
                return Ok();
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAccount(Guid id, UpdateAccountDto update)
        {
            var userInDb = repository.GetAccount(id);

            if (repository.GetAccount(id) == null)
                return NotFound();

            UserAccount updateUser = userInDb with
            {
                Name = update.Name,
                Email = update.Email,
                DayOfBirth = update.DayOfBirth,
                Address = update.Address
            };

            repository.UpdateAccount(updateUser);

            return Ok();
        }
    }
}
