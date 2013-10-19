using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;
using Medius.Schedulers;

namespace Medius.Integration.Platform.Setup
{
    public class Setup: SetupBase
    {
        public override void Run()
        {
            RegisterJobConfiguration("DummyIntegrationUpdate", typeof(Medius.Integration.Platform.ScheduledJobs.DummyUpdateTable).FullName, new DateTime(2013, 01, 01), new DateTime(2700, 01, 01), new TimeSpan(0,0,20));
            RegisterJobConfiguration("IntegrationQueueReaderJob", typeof(Medius.Integration.Platform.ScheduledJobs.IntegrationQueueReaderJob).FullName, new DateTime(2013, 01, 01), new DateTime(2700, 01, 01), new TimeSpan(0, 0, 20));
        }

        private void RegisterJobConfiguration(string name, string jobName, DateTime start, DateTime stop,
                                              TimeSpan repeat)
        {
            var jobConfiguration = Repository.Query<ScheduledJobConfiguration>(c => c.Name == name).SingleOrDefault();
            if (jobConfiguration == null)
            {
                jobConfiguration = new ScheduledJobConfiguration { Name = name, JobName = jobName, NextRunDate = start, StartDate = start, EndDate = stop, RepeatTime = repeat };
                Repository.DirtySave(ref jobConfiguration, refresh: false);
            }
        }
    }
}
