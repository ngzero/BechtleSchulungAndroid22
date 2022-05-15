using System;

namespace ECS_Webservice.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EntryStart { get; set; }
        public DateTime EntryEnd { get; set; }
    }
}