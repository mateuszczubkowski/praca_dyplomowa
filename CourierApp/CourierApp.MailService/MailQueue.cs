using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CourierApp.MailService
{
    public class MailQueue
    {
        private readonly List<MailDto> _mails = new List<MailDto>();
        private readonly ManualResetEventSlim _driverlock = new ManualResetEventSlim(true);

        public void EnqueueMail(MailDto dto)
        {
            _driverlock.Wait();
            _mails.Add(dto);

            _driverlock.Reset();
            _driverlock.Set();
        }

        public List<MailDto> GetAll()
        {
            _driverlock.Wait();
            var list = CopyList();
            _mails.Clear();

            _driverlock.Reset();
            _driverlock.Set();
            return list;
        }

        private List<MailDto> CopyList()
        {
            var list = new List<MailDto>();
            if (_mails.Any())
            {
                foreach (var mail in _mails)
                {
                    list.Add(mail);
                }
            }

            return list;
        }
    }
}
