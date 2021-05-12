using System;

namespace qsLogPack.Events
{
    public abstract class Message
    {
        protected Message()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid ApiKey { get; set; }    
        public DateTime Date {get; private set; }
    }
}