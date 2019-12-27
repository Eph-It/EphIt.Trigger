using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Repository
{
    public interface ITriggerRepository
    {
        Models.Trigger GetNextTrigger();
        ITrigger ConvertToITrigger(Models.Trigger trigger);
    }
}
