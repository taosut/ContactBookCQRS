using ContactBookCQRS.Domain.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ContactBookCQRS.Application.EventSourcedNormalizers
{
    public abstract class EventSourceNormatizer<THistoryData> where THistoryData : HistoryData, new()
    {
        public abstract IList<THistoryData> ToHistoryData(IList<StoredEvent> storedEvents);

        protected IList<THistoryData> GetHistoryData(IList<StoredEvent> storedEvents)
        {
            IList<THistoryData> historyData = new List<THistoryData>();

            foreach (var storedEvent in storedEvents)
            {
                THistoryData history = JsonConvert.DeserializeObject<THistoryData>(storedEvent.MetaData);
                history.Id = storedEvent.AggregateId.ToString();
                history.Timestamp = storedEvent.Timestamp.ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                if (storedEvent.MessageType.Contains("Created"))
                    history.Action = "Created";

                if (storedEvent.MessageType.Contains("Updated"))
                    history.Action = "Updated";

                if (storedEvent.MessageType.Contains("Deleted"))
                    history.Action = "Deleted";

                history.User = storedEvent.User;

                historyData.Add(history);
            }

            return historyData;
        }
    }
}
