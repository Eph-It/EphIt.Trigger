using EphIt.Trigger.Models;
using EphIt.Trigger.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EphIt.Trigger.Repository
{
    public class TriggerRepository : ITriggerRepository
    {
        private TriggerContext _triggerContext;
        public TriggerRepository(TriggerContext triggerContext)
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
                    return GetNextTrigger();
                }
            }
            return returnTrigger;
        }
        public ITrigger ConvertToITrigger(Models.Trigger trigger)
        {
            switch (trigger.Type)
            {
                case "Interval":
                    var it = new IntervalTrigger();
                    it.Initialize(trigger, _triggerContext);
                    return it;
            }
            throw new Exception("Could not find trigger type");
        }
    }
}
