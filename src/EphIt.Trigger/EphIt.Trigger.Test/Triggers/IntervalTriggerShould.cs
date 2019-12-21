using EphIt.Trigger.Models;
using EphIt.Trigger.Triggers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EphIt.Trigger.Test.Triggers
{
    public class IntervalTriggerShould
    {
        private IntervalTrigger _trigger;
        private Models.Trigger _model;
        public IntervalTriggerShould()
        {
            _trigger = new IntervalTrigger();
            _model = new Models.Trigger()
            {
                Interval = new Trigger_Interval()
                {
                    Interval = 1000
                }
            };
        }
        [Fact]
        public void ReturnTrueIfNoLastRun()
        {
            _trigger.Initialize(_model);
            var results = _trigger.Ready();
            Assert.True(results);
        }
        [Fact]
        public void ReturnTrueIfReadyToRun()
        {
            _model.LastRun = DateTime.UtcNow.AddTicks(-1500);
            _trigger.Initialize(_model);
            var results = _trigger.Ready();
            Assert.True(results);
        }
        [Fact]
        public void ReturnFalseIfNotReadyToRun()
        {
            _model.LastRun = DateTime.UtcNow.AddMinutes(1);
            _trigger.Initialize(_model);
            var results = _trigger.Ready();
            Assert.False(results);
        }
    }
}
