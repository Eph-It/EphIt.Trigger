using EphIt.Job.Models;
using EphIt.Job.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EphIt.Job.Repository
{
    public class TriggerRepository : ITriggerRepository
    {
        private JobContext _triggerContext;
        public TriggerRepository(JobContext triggerContext)
        {
            _triggerContext = triggerContext;
        }
        public Models.Trigger GetNextTrigger()
        {
            var returnTrigger = _triggerContext.Trigger
                        .Where(p =>
                            p.NextEvaluation.Value < DateTime.UtcNow
                            || p.NextEvaluation.HasValue == false
                        )
                        .OrderBy(p => p.NextEvaluation.Value)
                        .FirstOrDefault();
            if(returnTrigger != null)
            {
                returnTrigger.NextEvaluation = DateTime.UtcNow.AddSeconds(60);
                try
                {
                    _triggerContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // This exception means something modified the DB between getting return trigger
                    // and editing it. This may happen if multiple services all use the same DB so 
                    // just re-call this method to get the next one
                    return GetNextTrigger();
                }
            }
            return returnTrigger;
        }
        public ITrigger ConvertToITrigger(Models.Trigger trigger)
        {
            ITrigger _returnTrigger = null;
            switch (trigger.Type)
            {
                case "Interval":
                    _returnTrigger = new IntervalTrigger();
                    break;
            }
            if(_returnTrigger != null)
            {
                _returnTrigger.Initialize(trigger, _triggerContext);
                return _returnTrigger;
            }
            throw new Exception("Could not find trigger type");
        }
    }
}
