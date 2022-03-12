using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreateDate { get; }
    }
}
