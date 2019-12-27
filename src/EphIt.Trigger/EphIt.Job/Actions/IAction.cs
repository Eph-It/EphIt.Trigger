using EphIt.Job.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EphIt.Job.Actions
{
    public interface IAction
    {
        Task ExecuteAsync();
        void Initialize(Models.Action action, JobContext context);
    }
}
