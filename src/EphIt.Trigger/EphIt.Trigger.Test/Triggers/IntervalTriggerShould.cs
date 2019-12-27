using EphIt.Job.Models;
using EphIt.Job.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EphIt.Job.Test.Triggers
{
    public class IntervalTriggerShould
    {
        private IntervalTrigger _trigger;
        private Models.Trigger _model;
        private Models.JobContext _context;
        public IntervalTriggerShould()
        {
            _trigger = new IntervalTrigger();
            _model = new Models.Trigger()
            {
                TriggerId = 1
            };
            var interval = new Trigger_Interval()
            {
                TriggerId = 1,
                Interval = 100
            };
            var options = new DbContextOptionsBuilder<JobContext>()
                .UseInMemoryDatabase(databaseName: "TriggerDB")
                .Options;
            _context = new JobContext(options);
            _context.Trigger.Add(_model);
            _context.Trigger_Interval.Add(interval);
        }

        [Fact]
        public void ReturnTrueIfNoLastRun()
        {
            _trigger.Initialize(_model, _context);
            var results = _trigger.Ready();
            Assert.True(results);
        }
        [Fact]
        public void ReturnTrueIfReadyToRun()
        {
            _model.LastRun = DateTime.UtcNow.AddTicks(-1500);
            _trigger.Initialize(_model, _context);
            var results = _trigger.Ready();
            Assert.True(results);
        }
        [Fact]
        public void ReturnFalseIfNotReadyToRun()
        {
            _model.LastRun = DateTime.UtcNow.AddMinutes(1);
            _trigger.Initialize(_model, _context);
            var results = _trigger.Ready();
            Assert.False(results);
        }
        [Fact]
        public void NextEvaluationReturnsBasedOnCorrectInterval()
        {
            _model.Interval.Interval = 90000000;
            _model.TriggerInterval = 5;
            _trigger.Initialize(_model, _context);
            var results = _trigger.NextEvaluation();
            Assert.True(results > DateTime.UtcNow.AddTicks(_model.Interval.Interval - 1000000));
        }
    }
}
