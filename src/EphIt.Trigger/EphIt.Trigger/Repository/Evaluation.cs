using EphIt.Trigger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Repository
{
    public class Evaluation
    {
        private TriggerContext _triggerContext;
        public Evaluation(TriggerContext triggerContext)
        {
            _triggerContext = triggerContext;
        }
    }
}
