using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int? PlaceId { get; set; }
        public virtual ICollection<TaskReport> TaskReports { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual Place Place { get; set; }
        public Task()
        {
            TaskReports = new HashSet<TaskReport>();
            UserTasks = new HashSet<UserTask>();
        }
    }
}
