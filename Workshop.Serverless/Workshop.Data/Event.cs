using System;

namespace Workshop.Shared.Data
{
    public class Event
    {
        public string ProcessName { get; set; }
        public Guid ProcessInstanceId { get; set; }
        public string Step { get; set; }
        public string User { get; set; }
    }
}
