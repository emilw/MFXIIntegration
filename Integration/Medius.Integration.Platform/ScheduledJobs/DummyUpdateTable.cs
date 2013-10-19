using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Integration.Entities;
using Medius.Schedulers;

namespace Medius.Integration.Platform.ScheduledJobs
{
    public class DummyUpdateTable : ScheduledJobBase
    {
        public DummyUpdateTable()
        {

        }

        public override void RunJob(ScheduledJobConfiguration configuration)
        {
            var entity = new DummyEntity() { TestProperty = "Dude" };
            Repository.Save(ref entity);

        }
    }
}
