using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Application.EventSourcedNormalizers
{
    public class CategoryHistoryData : HistoryData
    {
        public string Name { get; set; }
    }
}
