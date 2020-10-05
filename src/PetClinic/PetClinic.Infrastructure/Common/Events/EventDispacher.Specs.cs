namespace PetClinic.Infrastructure.Common.Events
{
    using Application.Common;
    using Domain.Common;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class EventDispacherSpecs
    {
        [Fact]
        public async Task DispatchShouldDispatchEvents()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IEventHandler<TestEvent>, TestEventHandler>();

            var dispatcher = new EventDispatcher(serviceCollection.BuildServiceProvider());

            var domainEvent = new TestEvent();

            await dispatcher.Dispatch(domainEvent);

            Assert.True(domainEvent.Handled);
        }

        private class TestEvent : IDomainEvent
        {
            public bool Handled { get; set; }

            public DateTime OccurredOn => DateTime.Now;
        }

        private class TestEventHandler : IEventHandler<TestEvent>
        {
            public Task Handle(TestEvent domainEvent)
            {
                domainEvent.Handled = true;
                return Task.CompletedTask;
            }
        }
    }
}
