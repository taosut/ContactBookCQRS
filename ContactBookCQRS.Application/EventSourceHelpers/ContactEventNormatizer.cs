using ContactBookCQRS.Application.EventSourcedNormalizers;
using ContactBookCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactBookCQRS.Application.EventSourceHelpers
{
    public class ContactEventNormatizer : EventSourceNormatizer<ContactHistoryData>
    {
        public override IList<ContactHistoryData> ToHistoryData(IList<StoredEvent> storedEvents)
        {            
            IList<ContactHistoryData> historyData = GetHistoryData(storedEvents);
            var sorted = historyData.OrderBy(c => c.Timestamp);
            var contactHistory = new List<ContactHistoryData>();
            var last = new ContactHistoryData();

            foreach (var change in sorted)
            {
                var data = new ContactHistoryData();
                data.Id = change.Id == Guid.Empty.ToString() || 
                    change.Id == last.Id ? "" : change.Id;

                data.Name = string.IsNullOrWhiteSpace(change.Name) || 
                    change.Name == last.Name ? "" : change.Name;

                data.Email = string.IsNullOrWhiteSpace(change.Email) || 
                    change.Email == last.Email ? "" : change.Email;

                data.BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || 
                    change.BirthDate == last.BirthDate ? "" : change.BirthDate;
                data.BirthDate = DateTime.Parse(data.BirthDate).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                data.PhoneNumber = string.IsNullOrWhiteSpace(change.PhoneNumber) || 
                    change.PhoneNumber == last.PhoneNumber ? "" : change.PhoneNumber;

                data.Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action;
                data.Timestamp = change.Timestamp;
                data.User = change.User;

                contactHistory.Add(data);
                last = change;
            }

            return contactHistory;            
        }
    }
}
