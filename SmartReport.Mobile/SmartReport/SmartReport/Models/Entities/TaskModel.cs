using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.Models.Entities
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
