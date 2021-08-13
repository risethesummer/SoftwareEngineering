using System;
using BookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookBook.Dtos.Account;
using BookBook.Models;
using System.Security.Cryptography;
using System.Text;
using BookBook.Manager;
using System.Collections.Generic;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserAccountController : ControllerBase
    {
        private readonly IAccountRepository repository;
        private readonly IUserActivitiesManager activitiesManager;
        private readonly IResetPasswordManager resetPasswordManager;

        public UserAccountController(IAccountRepository repository, IUserActivitiesManager activitiesManager,
            IResetPasswordManager resetPasswordManager)
        {
            this.repository = repository;
            this.activitiesManager = activitiesManager;
            this.resetPasswordManager = resetPasswordManager;
        }
        
        // POST /register
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
                return Ok();
            }

            return BadRequest();
        }

        // POST /login
        [HttpPost("login")]
        public ActionResult LoginAccount(LoginAccountDto loginDto)
        {
            var password = MD5.HashData(Encoding.ASCII.GetBytes(loginDto.Password));
            var user = repository.GetAccount(loginDto.Account, password);
            if (user != null && activitiesManager.AddOnlineUser(user.ID))
                return new JsonResult(user);
            return BadRequest();
        }

        //GET /logout/id
        [HttpGet("logout/{id}")]
        public ActionResult LogoutAccount(Guid id)
        {
            activitiesManager.OffUser(id);
            return new EmptyResult();
        }

        [HttpGet]
        public IEnumerable<UserAccountDto> GetAccounts()
        {
            foreach (var account in repository.GetAccounts())
                yield return account.AsDto();
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult UpdateAccount(Guid id, UpdateAccountDto update)
        {
            var userInDb = repository.GetAccount(id);

            if (repository.GetAccount(id) == null)
                return NotFound();

            activitiesManager.UserActive(id);

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

        // POST /reset
        [HttpPost("reset")]
        public ActionResult ResetPassword(ResetPasswordDto resetDto)
        {
            var user = repository.GetAccount(resetDto.Account, resetDto.Email);
            if (user != null && resetPasswordManager.AddRequest(user.ID, user.Email))
                return Content(user.ID.ToString());
            return BadRequest("The account or email do not exist");
        }

        // DELETE /reset
        [HttpDelete("reset/{id}")]
        public ActionResult ResetPasswordConfirm(Guid id, int confirmCode)
        {
            if (resetPasswordManager.ConfirmMailCode(id, confirmCode))
                return Ok();
            return BadRequest();
        }

        [HttpPut("reset/{id}")]
        public ActionResult ChangeNewPassword(Guid id, string newPassword)
        {
            var password = MD5.HashData(Encoding.ASCII.GetBytes(newPassword));
            if (repository.UpdateAccount(id, password))
                return Ok();
            return BadRequest();
        }
    }
}
