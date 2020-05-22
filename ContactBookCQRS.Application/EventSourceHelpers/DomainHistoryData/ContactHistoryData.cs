using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Application.EventSourcedNormalizers
{
    public class ContactHistoryData : HistoryData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
