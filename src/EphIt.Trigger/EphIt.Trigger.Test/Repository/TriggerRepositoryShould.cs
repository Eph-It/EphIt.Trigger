using EphIt.Trigger.Models;
using EphIt.Trigger.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using EntityFrameworkCore3Mock;
using EphIt.Trigger.Triggers;

namespace EphIt.Trigger.Test.Repository
{
    public class TriggerRepositoryShould
    {
        private TriggerContext _context;
        private DbContextMock<TriggerContext> _mockContext;
        private TriggerRepository _triggerRepo;
        private TriggerRepository _mockTriggerRepo;
        public TriggerRepositoryShould()
        {
            var options = new DbContextOptionsBuilder<TriggerContext>()
                .UseInMemoryDatabase(databaseName: "TriggerDB")
                .Options;
            _context = new TriggerContext(options);
            _triggerRepo = new TriggerRepository(_context);
            _mockContext = new DbContextMock<TriggerContext>();
            _mockTriggerRepo = new TriggerRepository(_mockContext.Object);
        }
        [Fact]
        public void GetNextTriggerAndUpdateLastEvaluated()
        {

            // Setup
            var lastEvaluation = DateTime.UtcNow.AddMinutes(-5);
            var nextEvaluation = DateTime.UtcNow.AddSeconds(-5);
            var triggerId = 3;
            var nextTrigger = new Models.Trigger()
            {
                NextEvaluation = nextEvaluation,
                TriggerId = triggerId,
                LastEvaluation = lastEvaluation
            };
            _context.Trigger.Add(nextTrigger);
            _context.SaveChanges();

            // Evaluate
            var results = _triggerRepo.GetNextTrigger();

            // Test
            Assert.Equal(results.TriggerId, triggerId);
            Assert.NotEqual(results.NextEvaluation, nextEvaluation);
        }

        [Fact]
        public void GetNextTriggerShouldHandleConcurrency()
        {
            // set up
            var trigger1 = new Models.Trigger()
            {
                NextEvaluation = DateTime.UtcNow.AddYears(-5),
                TriggerId = 4,
                LastEvaluation = DateTime.UtcNow.AddMinutes(-5)
            };
            var trigger2 = new Models.Trigger()
            {
                NextEvaluation = DateTime.UtcNow.AddYears(-3),
                TriggerId = 5,
                LastEvaluation = DateTime.UtcNow.AddMinutes(-5)
            };

            var triggerList = new []
            {
                trigger1, trigger2
            };
            var triggerDbSetMock = _mockContext.CreateDbSetMock(x => x.Trigger, triggerList);
            var saveChangesCount = 0;
            _mockContext.Setup(x => x.SaveChanges()).Callback(() => 
            {
                saveChangesCount++;
                if(saveChangesCount == 1)
                {
                    triggerList[0].NextEvaluation = DateTime.UtcNow.AddSeconds(3);
                    _mockContext.Reset();
                    triggerDbSetMock = _mockContext.CreateDbSetMock(x => x.Trigger, triggerList);
                    throw new DbUpdateException();
                }
            });
            // run

            var results = _mockTriggerRepo.GetNextTrigger();

            // test

            Assert.Equal(results.TriggerId, trigger2.TriggerId);

        }
        [Fact]
        public void ConvertToITriggerShouldWorkForInterval()
        {
            // setup
            var trigger1 = new Models.Trigger()
            {
                NextEvaluation = DateTime.UtcNow.AddYears(5),
                TriggerId = 4,
                LastEvaluation = DateTime.UtcNow.AddMinutes(5),
                Type = "Interval"
            };
            _context.Trigger.Add(trigger1);
            _context.SaveChanges();
            // execution
            var results = _triggerRepo.ConvertToITrigger(trigger1);

            // Test
            Assert.IsType<IntervalTrigger>(results);
        }
    }
}
