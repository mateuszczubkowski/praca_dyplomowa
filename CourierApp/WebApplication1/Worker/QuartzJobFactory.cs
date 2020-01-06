using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace CourierApp.WebApp.Worker
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly List<KeyValuePair<IJob, IServiceScope>> _jobScopeDictionary;

        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _jobScopeDictionary = new List<KeyValuePair<IJob, IServiceScope>>();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var scope = _serviceProvider.CreateScope();

            var job = scope.ServiceProvider.GetService(jobDetail.JobType) as IJob;

            _jobScopeDictionary.Add(new KeyValuePair<IJob, IServiceScope>(job, scope));

            return job;
        }

        public void ReturnJob(IJob job)
        {
            var pair = _jobScopeDictionary.FirstOrDefault(x => x.Key == job);
            (job as IDisposable)?.Dispose();

            pair.Value?.Dispose();
        }

    }
}
