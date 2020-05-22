using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Application.EventSourcedNormalizers
{
    public class HistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public string User { get; set; }
    }
}
