using EphIt.Trigger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Triggers
{
    public class IntervalTrigger : ITrigger
    {
        private Models.Trigger _model;
        public void Initialize(Models.Trigger model)
        {
            _model = model;
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
    }
}
