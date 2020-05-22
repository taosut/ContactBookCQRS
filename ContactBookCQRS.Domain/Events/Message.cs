using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ContactBookCQRS.Domain.Events
{
    public abstract class Message : IRequest<bool>
    {
        [JsonIgnore]
        public string MessageType { get; protected set; }
        [JsonIgnore]
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
