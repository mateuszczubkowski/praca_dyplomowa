﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CourierApp.MailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _config;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _config = emailConfig.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = GetMessage(message, email, subject);

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_config.MailServerAddress, Convert.ToInt32(_config.MailServerPort));

                await client.AuthenticateAsync(_config.UserId, _config.UserPassword);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        private MimeMessage GetMessage(string textMessage, string email, string subject)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_config.FromName, _config.FromAddress));
            message.To.Add(new MailboxAddress(string.Empty, email));
            message.Subject = subject;

            var body = new TextPart("plain")
            {
                Text = textMessage
            };

            var multipart = new Multipart("mixed");

            multipart.Add(body);

            message.Body = multipart;

            return message;
        }
    }
}
