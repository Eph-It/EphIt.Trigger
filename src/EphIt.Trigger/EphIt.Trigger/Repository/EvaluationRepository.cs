using EphIt.Trigger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Repository
{
    public class EvaluationRepository
    {
        private TriggerContext _triggerContext;
        private ITriggerRepository _triggerRepo;
        public EvaluationRepository(TriggerContext triggerContext, ITriggerRepository triggerRepository)
        {
            _triggerContext = triggerContext;
            _triggerRepo = triggerRepository;
        }

        public void EvaluateTriggers()
        {
            // First get the next trigger's DB object
            var nextTrigger = _triggerRepo.GetNextTrigger();
            if(nextTrigger != null)
            {
                // then convert it to an ITrigger so we have access to shared methods
                var iTrigger = _triggerRepo.ConvertToITrigger(nextTrigger);

                // Update the next run information
                nextTrigger.NextEvaluation = iTrigger.NextEvaluation();
                nextTrigger.LastEvaluation = DateTime.UtcNow;
                var asyncSave = _triggerContext.SaveChangesAsync();

                // Check if the trigger is ready to run the action
                if (iTrigger.Ready())
                {
                    // RunAction
                }

                // wait for DB changes to take effect before releasing
                asyncSave.Wait();
            }
        }
    }
}
