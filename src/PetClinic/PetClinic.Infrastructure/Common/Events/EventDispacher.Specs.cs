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
            serviceCollection.AddTransient<IEventHandler<ArticleEvent>, ArticleEventHandler>();

            var dispatcher = new EventDispatcher(serviceCollection.BuildServiceProvider());

            var domainEvent = new ArticleEvent();

            await dispatcher.Dispatch(domainEvent);

            Assert.True(domainEvent.Handled);
        }

        private class ArticleEvent : IDomainEvent
        {
            public bool Handled { get; set; }

            public DateTime OccurredOn => DateTime.Now;
        }

        private class ArticleEventHandler : IEventHandler<ArticleEvent>
        {
            public Task Handle(ArticleEvent domainEvent)
            {
                domainEvent.Handled = true;
                return Task.CompletedTask;
            }
        }
    }
}
