using EphIt.Trigger.Models;
using System;

namespace EphIt.Trigger
{
    /// <summary>
    /// The core Trigger interface for defining when an action should run.
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// Sets up the trigger with the model. Models are all inherited from
        /// TriggerModel which creates a base set of properties
        /// </summary>
        /// <param name="model">Creates a base set of properties to query EF</param>
        void Initialize(Models.Trigger model, Models.TriggerContext context);
        /// <summary>
        /// Evalutes the trigger's condition to see if we should run yet
        /// </summary>
        /// <returns>True if the corresponding action should be run</returns>
        bool Ready();
        /// <summary>
        /// Next time this trigger should evaluate
        /// </summary>
        /// <returns>Next trigger evaluation</returns>
        DateTime NextEvaluation();
    }
}
