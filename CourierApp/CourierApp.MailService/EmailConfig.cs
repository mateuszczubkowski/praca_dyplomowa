using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.MailService
{
    public class EmailConfig
    {
        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public string MailServerAddress { get; set; }

        public string MailServerPort { get; set; }

        public string UserId { get; set; }

        public string UserPassword { get; set; }

        public string AdminEmail { get; set; }
    }
}
