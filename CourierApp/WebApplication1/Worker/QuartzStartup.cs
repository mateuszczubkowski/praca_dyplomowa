using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace CourierApp.WebApp.Worker
{
    public class QuartzStartup
    {
        private readonly IServiceProvider _container;
        private IScheduler _scheduler;

        public QuartzStartup(IServiceProvider container)
        {
            _container = container;
        }

        public void Start()
        {
            if (_scheduler != null)
                throw new InvalidOperationException("Already started.");

            var properties = new NameValueCollection
            {
                ["quartz.serializer.type"] = "json",
                ["quartz.scheduler.instanceName"] = "CourierAppScheduler",
                ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
                ["quartz.threadPool.threadCount"] = "4"
            };

            var schedulerFactory = new StdSchedulerFactory(properties);
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = (IJobFactory)_container.GetService(typeof(IJobFactory));
            _scheduler.Start().Wait();

            var emailJob = JobBuilder.Create<MailJob>()
               .WithIdentity("MailJob")
               .Build();
            var mailTrigger = TriggerBuilder.Create()
                .WithIdentity("MailCron")
                .StartNow()
                .WithCronSchedule("0 0/5 * * * ?")
                .Build();

            _scheduler.ScheduleJob(emailJob, mailTrigger).Wait();
        }

        public void Stop()
        {
            if (_scheduler == null)
                return;

            // give running jobs 30 sec (for example) to stop gracefully
            if (_scheduler.Shutdown(true).Wait(30000))
            {
                _scheduler = null;
            }
        }
    }
}
