using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.MailService;
using Quartz;

namespace CourierApp.WebApp.Worker
{
    public class MailJob : IJob
    {
        private readonly MailQueue _mailQueue;
        private readonly IEmailService _emailService;

        public MailJob(MailQueue mailQueue, IEmailService emailService)
        {
            _mailQueue = mailQueue;
            _emailService = emailService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var emails = _mailQueue.GetAll();

            foreach (var mailDto in emails)
            {
                _emailService.SendEmailAsync(mailDto.Address, mailDto.Subject, mailDto.Message);
            }

            return Task.CompletedTask;
        }
    }
}
