using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Entities
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string ReportText { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TaskReport> TaskReports { get; set; }
        public Report()
        {
            TaskReports = new HashSet<TaskReport>();
        }
    }
}
