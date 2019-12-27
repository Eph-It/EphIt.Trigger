using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Job.Repository
{
    public interface ITriggerRepository
    {
        Models.Trigger GetNextTrigger();
        ITrigger ConvertToITrigger(Models.Trigger trigger);
    }
}
