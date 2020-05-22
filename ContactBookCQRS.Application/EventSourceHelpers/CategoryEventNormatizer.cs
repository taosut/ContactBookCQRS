using ContactBookCQRS.Application.EventSourcedNormalizers;
using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactBookCQRS.Application.EventSourceHelpers
{
    public class CategoryEventNormatizer : EventSourceNormatizer<CategoryHistoryData>
    {
        public override IList<CategoryHistoryData> ToHistoryData(IList<StoredEvent> storedEvents)
        {            
            IList<CategoryHistoryData> historyData = GetHistoryData(storedEvents);
            var sorted = historyData.OrderBy(c => c.Timestamp);
            var categoryHistory = new List<CategoryHistoryData>();
            var last = new CategoryHistoryData();

            foreach (var change in sorted)
            {
                var data = new CategoryHistoryData();
                data.Id = change.Id == Guid.Empty.ToString() || 
                    change.Id == last.Id ? "" : change.Id;

                data.Name = string.IsNullOrWhiteSpace(change.Name) || 
                    change.Name == last.Name ? "" : change.Name;

                data.Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action;
                data.Timestamp = change.Timestamp;
                data.User = change.User;

                categoryHistory.Add(data);
                last = change;
            }

            return categoryHistory;            
        }

    }
}
