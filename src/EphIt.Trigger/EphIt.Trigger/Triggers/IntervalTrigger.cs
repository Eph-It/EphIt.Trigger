using EphIt.Trigger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Triggers
{
    public class IntervalTrigger : ITrigger
    {
        private Models.Trigger _model;
        private Models.TriggerContext _context;
        public void Initialize(Models.Trigger model, Models.TriggerContext context)
        {
            _model = model;
            _context = context;
            _context.Entry(model).Reference(p => p.Interval).Load();
        }

        public bool Ready()
        {
            if (_model.LastRun.HasValue)
            {
                if(_model.LastRun.Value.AddTicks(_model.Interval.Interval) > DateTime.UtcNow)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
        public DateTime NextEvaluation()
        {
            return (DateTime.UtcNow.AddTicks(_model.Interval.Interval));
        }

    }
}
