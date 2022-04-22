using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BookBook.Repositories;

namespace BookBook.Manager
{
    public class ResetPasswordManager : IResetPasswordManager
    {
        private readonly IResetPasswordRepository repository;

        private const int MAX_TIME_MINUTES = 5;

        public ResetPasswordManager(IResetPasswordRepository repository)
        {
            this.repository = repository;
        }

        public bool AddRequest(Guid id, string email)
        {
            var request = repository.GetRequest(id);
            if (request == null || (DateTime.Now - request.Time).TotalMinutes >= MAX_TIME_MINUTES)
            {
                if (request != null)
                    repository.DeleteReset(id);

                int sendCode = SendMail(email);
                if (sendCode != -1)
                {
                    repository.AddReset(new Models.ResetPasswordRequest()
                    {
                        UserId = id,
                        Code = sendCode,
                        Time = DateTime.Now
                    });

                    return true;
                }
            }

            return false;
        }

        public bool ConfirmMailCode(Guid id, int mailCode)
        {
            var request = repository.GetRequest(id, mailCode);
            if (request != null)
            {
                repository.DeleteReset(id);
                //If match the code and have time < 5 minutes
                if ((DateTime.Now - request.Time).TotalMinutes < MAX_TIME_MINUTES)
                    return true;
            }
            return false;
        }

        static int SendMail(string email)
        {
            try
            {
                var ran = new Random(DateTime.Now.Millisecond);
                int code = ran.Next(100000, 1000000);


                using MailMessage message = new()
                {
                    From = new MailAddress("bookbooksoftwareengineering@gmail.com", "Book Book"),
                    Subject = "Code to reset password",
                    Body = "Here is the code to reset your password: " + code.ToString(),
                    Priority = MailPriority.High
                };

                using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential("bookbooksoftwareengineering@gmail.com", "bookbook1!");
              
                message.To.Add(email);

                smtpClient.Send(message);

                return code;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
